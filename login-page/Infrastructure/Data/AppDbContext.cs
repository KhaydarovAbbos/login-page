using login_page.Entities.Products;
using login_page.Entities.Shops;
using login_page.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace login_page.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Shop> Shop { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server = localhost; username = root; database=login_page");
        }

    }
}
