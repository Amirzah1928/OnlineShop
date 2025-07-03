
using OnlineShop.DomainService.Repositories;

namespace OnlineShop.DomainService.Data
{
    public interface IReadUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken);

        public IUserReadRepository UserRepository { get; init; }
    }
}
