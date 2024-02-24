using RedisAPI.services;
using RedisExampleApp.API.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisExampleApp.API.Repository
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private readonly IProductRepository _repository;
        private readonly RedisService _redisService;
        private readonly IDatabase _redisDatabase;
        private readonly string redisKey = "productCache";

        public ProductRepositoryWithCache(IProductRepository repository, RedisService redisService)
        {
            _repository = repository;
            _redisService = redisService;
            _redisDatabase = _redisService.GetDb(0);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = await _repository.CreateAsync(product);
            await _redisDatabase.HashSetAsync(redisKey, product.Id, JsonSerializer.Serialize(newProduct));

            return newProduct;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            if (!await _redisDatabase.KeyExistsAsync(redisKey))
                return await LoadToCacheFromDbAsync();

            List<Product> productList = new();
            var productSerializeList = await _redisDatabase.HashGetAllAsync(redisKey);
            productSerializeList.ToList().ForEach(x =>
            {
                var product = JsonSerializer.Deserialize<Product>(x.Value);
                productList.Add(product);
            });

            return productList;
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            if (await _redisDatabase.KeyExistsAsync(redisKey))
            {
                var product = await _redisDatabase.HashGetAsync(redisKey, Id);
                return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : null;
            }

            var products = await LoadToCacheFromDbAsync();

            return products.Find(x => x.Id == Id);
        }

        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products = await _repository.GetAllAsync();

            products.ForEach(async product =>
            {
                await _redisDatabase.HashSetAsync(redisKey, product.Id, JsonSerializer.Serialize(product));
            });

            return products;

        }
    }
}
