
using OnlineShop.Repositories;

namespace OnlineShop.Data
{
    public class UnitOfWork(OnlineShopDBContext db,IUserRepository userRepository,ICityRepository cityRepository) : IUnitOfWork
    {
        public IUserRepository userRepository { get; init; } = userRepository;
        public ICityRepository cityRepository { get; init; } = cityRepository;

        public async Task CommitAsync(CancellationToken cancellation)
        {
           await db.SaveChangesAsync(cancellation);
        }

    }
}
