using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public class UserRepository(OnlineShopDBContext db) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellation)
        {
            await db.Users.AddAsync(user, cancellation);
        }

        public void Delete(User user)
        {
            db.Entry(user).Property("IsDeleted").CurrentValue = true;
            db.Entry(user).State = EntityState.Modified;
        }


        public void Update(User user)
        {
            db.Users.Update(user);
        }


        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellation)
        {
           return await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellation);
        }

        public async Task<List<User>> GetListAsync(string? q, CancellationToken cancellation)
        {
            var query = db.Users.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(q))
                query = query.Where(x => x.FirstName.Contains(q) || x.LastName.Contains(q));

            return await query.ToListAsync(cancellation);
        }


    }
}
