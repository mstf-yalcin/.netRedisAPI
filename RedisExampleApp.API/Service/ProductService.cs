﻿using RedisExampleApp.API.Models;
using RedisExampleApp.API.Repository;

namespace RedisExampleApp.API.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _repository.CreateAsync(product);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            return await _repository.GetByIdAsync(Id);
        }
    }
}
