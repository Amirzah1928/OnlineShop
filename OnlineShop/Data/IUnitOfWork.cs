using OnlineShop.Repositories;

namespace OnlineShop.Data
{
    public interface IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellation);
        public IUserRepository userRepository { get; init; }
        public ICityRepository cityRepository { get; init; }
    }
}
