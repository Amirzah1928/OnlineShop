
using OnlineShop.DomainModel.Models;

namespace OnlineShop.DomainService.Repositories
{
    public interface ICityRepository
    {
        public Task<List<City>> GetCitiesListAsync(CancellationToken cancellation);
    }
}
