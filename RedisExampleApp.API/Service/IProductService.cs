using RedisExampleApp.API.Models;

namespace RedisExampleApp.API.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int Id);

        Task<Product> CreateAsync(Product product);
    }
}
