using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAPI.model;
using RedisAPI.services;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisAPI.Controllers
{
 
    public class HomeController : BaseController
    {
        public HomeController(RedisService redisService) : base(redisService)
        {
        }

        [HttpGet("AddCache")]
        public void AddCache()
        {

            _redisDb.StringSet("name", "Test");
            _redisDb.StringSet("number", 50);
        }

        [HttpGet("GetCache")]
        public string GetCache()
        {
            _redisDb.StringIncrement("number");
            _redisDb.StringIncrement("number", 10);

            var name = _redisDb.StringGet("name");
            var number = _redisDb.StringGet("number");

            if (name.HasValue && number.HasValue)
                return "name: " + name.ToString() + " number:" + number.ToString();

            return "";
        }

        [HttpGet("AddProductCache")]
        public void AddProductCache()
        {
            var product = new Product(1, "name", "descrb");

            var serializeProduct = JsonSerializer.Serialize(product);

            _redisDb.StringSet($"product:{product.Id}", serializeProduct);
        }


        [HttpGet("GetProductCache")]
        public async Task<Product> GetProductCache()
        {
            var serializeProduct = await _redisDb.StringGetAsync("product:1");

            Product product = null;

            if (serializeProduct.HasValue)
                product = JsonSerializer.Deserialize<Product>(serializeProduct);
            return product;

        }

    }
}
