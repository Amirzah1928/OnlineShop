using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public interface IUserRepository
    {
        public Task AddAsync(User user, CancellationToken cancellation);
        public void Update(User user);
        public void Delete(User user);
        public Task<User?> GetByIdAsync(int id,CancellationToken cancellation);
        public Task<List<User>> GetListAsync(string? q,CancellationToken cancellation);
    }
}
