﻿using Microsoft.EntityFrameworkCore;
using RedisExampleApp.API.Models;

namespace RedisExampleApp.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Add<Product>(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }
    }
}
