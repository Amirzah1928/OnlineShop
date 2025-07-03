using Microsoft.EntityFrameworkCore;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.Repositories;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Helper;


namespace OnlineShop.Infrastructure.Repositories
{
    public class UserRepository(OnlineShopDBContext db) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellation)
        {
            await db.Users.AddAsync(user, cancellation);
            user.Created();
        }

        public void Delete(User user)
        {
            db.Users.Update(user);
            user.Deleted();
        }


        public void Update(User user)
        {
            db.Users.Update(user);
            user.Updated();
        }


        public void Update(List<User> users)
        {
            db.Users.UpdateRange(users);
        }


        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return await db.Users
            .Include(x => x.Options)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id, cancellation);
        }


        public async Task<(int, List<User>)> GetListAsync(BaseSpecification<User> specification, CancellationToken cancellation)
        {
            var query = db.Users.AsNoTracking().Specify(specification);

            var totalCount = await query.CountAsync(cancellation);
            if (specification.IsPaginationEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            var data = await query.ToListAsync(cancellation);

            return (totalCount, data);
        }


        public async Task<User?> GetByTrackingCodeAsync(BaseSpecification<User> specification, CancellationToken cancellation)
        {
            var query = db.Users.AsNoTracking().Specify(specification);

            var user = await query.FirstOrDefaultAsync(cancellation);
            return user;
        }

    }
}
