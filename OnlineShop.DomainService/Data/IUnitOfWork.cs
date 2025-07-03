using OnlineShop.DomainService.Repositories;

namespace OnlineShop.DomainService.Data
{
    public interface IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellation);
        public IUserRepository UserRepository { get; init; }
        public ICityRepository CityRepository { get; init; }
    }
}
