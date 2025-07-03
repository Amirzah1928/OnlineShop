using Microsoft.EntityFrameworkCore;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.Repositories;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Helper;

namespace OnlineShop.Infrastructure.Repositories
{
    public class UserReadRepository(OnlineShopReadDBContext db) : IUserReadRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await db.Users.AddAsync(user, cancellationToken);
        }



        public void Delete(User user)
        {
            db.Users.Remove(user);
        }




        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }



        public async Task<(int, List<User>)> GetListAsync(BaseSpecification<User> specification, CancellationToken cancellationToken)
        {
            var query = db.Users.AsNoTracking().Specify(specification);

            var totalCount = 0;
            if (specification.IsPaginationEnabled)
            {
                totalCount = await query.CountAsync(cancellationToken);

                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            var data = await query.ToListAsync(cancellationToken);

            return (totalCount, data);
        }



        public void Update(User user)
        {
            db.Users.Update(user);
        } 
        
        
        
        public void Update(List<User> user)
        {
            db.Users.UpdateRange(user);
        }
    }
}
