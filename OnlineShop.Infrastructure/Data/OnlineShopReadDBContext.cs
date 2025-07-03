
using Microsoft.EntityFrameworkCore;
using OnlineShop.DomainModel.Models;

namespace OnlineShop.Infrastructure.Data
{
    public class OnlineShopReadDBContext(DbContextOptions<OnlineShopReadDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<User>().Property(x => x.FirstName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.LastName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.PhoneNumber).HasMaxLength(11);

            modelBuilder.Entity<User>().Property(x => x.IsActive)
                .HasConversion(
                    v => v.ToString(),
                    v => bool.Parse(v)
                ).HasMaxLength(50);

            modelBuilder.Entity<User>().Property(x => x.Coordinate)
           .HasConversion(
               v => v.Latitude + "," + v.Longitude,
               v => ConvertStringToCoordinate(v)
           ).HasMaxLength(50);

            modelBuilder.Entity<User>().Ignore(x => x.Options);
            modelBuilder.Entity<User>().Ignore(x => x.Tags);
            modelBuilder.Entity<User>().Ignore(x => x.CreatedAt);
            modelBuilder.Entity<User>().Ignore(x => x.UpdatedAt);
            modelBuilder.Entity<User>().Ignore(x => x.DeletedAt);
            modelBuilder.Entity<User>().Ignore(x => x.IsDeleted);

        }


        private static Coordinate ConvertStringToCoordinate(string v)
        {
            return new Coordinate
            {
                Latitude = double.Parse(v.Split(',')[0]),
                Longitude = double.Parse(v.Split(',')[1]),
            };
        }
    }
}
