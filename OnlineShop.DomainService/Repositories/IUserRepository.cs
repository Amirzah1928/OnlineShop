
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;

namespace OnlineShop.DomainService.Repositories
{
    public interface IUserRepository
    {
        public Task AddAsync(User user, CancellationToken cancellation);
        public void Update(User user);
        public void Update(List<User> users);
        public void Delete(User user);
        public Task<User?> GetByIdAsync(int id,CancellationToken cancellation);
        public Task<(int ,List<User>)> GetListAsync(BaseSpecification<User> specification, CancellationToken cancellation);
        public Task<User?> GetByTrackingCodeAsync(BaseSpecification<User> specification, CancellationToken cancellation);
    }
}
