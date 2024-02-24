using Microsoft.EntityFrameworkCore;

namespace RedisExampleApp.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "name1", Description = "desc1" },
                new Product() { Id = 2, Name = "name2", Description = "desc2" },
                new Product() { Id = 3, Name = "name3", Description = "desc3" },
                new Product() { Id = 4, Name = "name4", Description = "desc4" });


            base.OnModelCreating(modelBuilder);
        }

    }
}
