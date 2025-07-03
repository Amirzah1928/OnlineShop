using Microsoft.EntityFrameworkCore;
using OnlineShop.DomainModel.Models;

namespace OnlineShop.Infrastructure.Data
{
    public class OnlineShopDBContext(DbContextOptions<OnlineShopDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("Increase3Id").StartsAt(10).IncrementsBy(3);


            #region User
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.Increase3Id");
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.LastName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.PhoneNumber).HasMaxLength(11);



            modelBuilder.Entity<User>().Property(x => x.IsActive).
                HasConversion(
                    x => x.ToString(),
                    x => bool.Parse(x)
                ).HasMaxLength(5);

            modelBuilder.Entity<User>().Property(x => x.Coordinate)
           .HasConversion(
               v => v.Latitude + "," + v.Longitude,
               v => ConvertStringToCoordinate(v)
           ).HasMaxLength(50);

            modelBuilder.Entity<User>().Property(x => x.TrackingCode).HasMaxLength(10);



            modelBuilder.Entity<User>().HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);



            modelBuilder.Entity<User>()
           .HasMany(x => x.Options)
           .WithOne()
           .HasForeignKey("UserId").IsRequired() // Shadow property
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserOption>().HasKey(x => x.Id);
            modelBuilder.Entity<UserOption>().Property(x => x.Description).HasMaxLength(100);
            modelBuilder.Entity<UserOption>().ToTable("UserOptions");


            modelBuilder.Entity<User>()
            .OwnsMany(x => x.Tags, tag =>
            {
                tag.WithOwner().HasForeignKey("UserId"); // Shadow property
                tag.Property(x => x.Title).HasMaxLength(20);
                tag.Property(x => x.Priority).IsRequired();
                tag.HasKey("UserId", "Title", "Priority");
                tag.ToTable("UserTags");
            });

            #endregion



            #region City

            modelBuilder.Entity<City>().HasKey(x => x.Id);
            modelBuilder.Entity<City>().Property(x => x.Name).HasMaxLength(50);


            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Tehran" },
                new City { Id = 2, Name = "Mashhad" },
                new City { Id = 3, Name = "Shiraz" },
                new City { Id = 4, Name = "Karaj" }
                );

            #endregion


            base.OnModelCreating(modelBuilder);
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
