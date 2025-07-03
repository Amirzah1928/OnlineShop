
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Repositories;

namespace OnlineShop.Infrastructure.Data
{
    public class ReadUnitOfWork(OnlineShopReadDBContext db, IUserReadRepository userReadRepository) : IReadUnitOfWork
    {
        public IUserReadRepository UserRepository { get; init; } = userReadRepository;

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
