using Microsoft.EntityFrameworkCore;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Repositories;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CityRepository(OnlineShopDBContext db) : ICityRepository
    {
        public async Task<List<City>> GetCitiesListAsync(CancellationToken cancellation)
        {
            return await db.Cities.ToListAsync(cancellation); 
        }
    }
}
