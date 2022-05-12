using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Models
{
    public class Store :IdentityDbContext<User>
    {
        public Store()
        {
                
        }
        public Store(DbContextOptions options) :base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product{ get; set; }
        public DbSet<Cart> Cart{ get; set; }
        public DbSet<CartItem> CartItem{ get; set; }
        public DbSet<Order> Order{ get; set; }
        public DbSet<User> UserApp { get; set; }

    }
}
