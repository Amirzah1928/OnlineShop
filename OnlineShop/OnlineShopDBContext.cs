using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop
{
    public class OnlineShopDBContext(DbContextOptions<OnlineShopDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
