using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Repositories;

namespace OnlineShop.Infrastructure.Data
{
    public class UnitOfWork(OnlineShopDBContext db,IUserRepository UserRepository,ICityRepository cityRepository) : IUnitOfWork
    {
        public IUserRepository UserRepository { get; init; } = UserRepository;
        public ICityRepository CityRepository { get; init; } = cityRepository;

        public async Task CommitAsync(CancellationToken cancellation)
        {
           await db.SaveChangesAsync(cancellation);
        }

    }
}
