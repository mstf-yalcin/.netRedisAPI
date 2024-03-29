﻿using RedisExampleApp.API.Models;

namespace RedisExampleApp.API.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int Id);

        Task<Product> CreateAsync(Product product);
    }
}
