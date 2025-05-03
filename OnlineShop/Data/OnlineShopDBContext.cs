using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Data
{
    public class OnlineShopDBContext(DbContextOptions<OnlineShopDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("Increase3By3").StartsAt(10).IncrementsBy(3);


            #region User
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEXT VALUE FOR dbo.Increase2By2");
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.LastName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.PhoneNumber).HasMaxLength(11);

            modelBuilder.Entity<User>().Property(x => x.Isactive).
                HasConversion(
                    x => x.ToString(),
                    x => bool.Parse(x)
                ).HasMaxLength(5);

            modelBuilder.Entity<User>().Property(x => x.Coordinate)
           .HasConversion(
               v => v.Latitude + "," + v.Longitude,
               v => ConvertStringToCoordinate(v)
           ).HasMaxLength(50);



            modelBuilder.Entity<User>().Property<DateTime>("CreatedAt").IsRequired(true);
            modelBuilder.Entity<User>().Property<DateTime?>("UpdatedAt").IsRequired(false);
            modelBuilder.Entity<User>().Property<DateTime?>("DeletedAt").IsRequired(false);


            modelBuilder.Entity<User>().Property<bool>("IsDeleted").HasDefaultValue(false);



            modelBuilder.Entity<User>().HasQueryFilter(e => EF.Property<bool>(e,"IsDeleted") == false);

            #endregion



            #region City

            modelBuilder.Entity<City>().HasKey(x => x.Id);
            modelBuilder.Entity<City>().Property(x => x.Name).HasMaxLength(50);


            modelBuilder.Entity<City>().HasData(
                new City {Id=1, Name ="Tehran"},
                new City {Id=2, Name ="Mashhad"},
                new City {Id=3, Name ="Shiraz"},
                new City {Id=4, Name ="Karaj"}
                );

            #endregion


            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            SetCreatedAt();
            SetUpdatedAt();
            SetDeletedAt();
            return await base.SaveChangesAsync(cancellation);
        }




        private void SetCreatedAt()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.Metadata.FindProperty("CreatedAt") != null)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
            }
        }

        private void SetUpdatedAt()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Metadata.FindProperty("UpdatedAt") != null)
                {
                    var isDeletedProp = entry.Metadata.FindProperty("IsDeleted");

                    if (isDeletedProp != null)
                    {
                        var isDeleted = (bool?)entry.Property("IsDeleted").CurrentValue;
                        if (isDeleted == true)
                            continue;
                    }

                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }
        }


        private void SetDeletedAt()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Metadata.FindProperty("IsDeleted") != null &&
                    entry.Metadata.FindProperty("DeletedAt") != null)
                {
                    entry.Property("DeletedAt").CurrentValue = DateTime.Now;
                }
            }
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
