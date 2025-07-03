using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;


namespace OnlineShop.DomainService.Repositories
{
    public interface IUserReadRepository
    {
        public Task AddAsync(User user, CancellationToken cancellationToken);
        public void Update(User user);
        public void Update(List<User> user);
        public void Delete(User user);
        public Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<(int, List<User>)> GetListAsync(BaseSpecification<User> specification, CancellationToken cancellationToken);
    }
}
