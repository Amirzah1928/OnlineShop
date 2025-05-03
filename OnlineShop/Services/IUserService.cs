using OnlineShop.DTOs;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Services
{
    public interface IUserService
    {
        public Task CreateAsync(CreateUserDto newUser, CancellationToken cancellation);
        public Task UpdateAsync(int id, UpdateUserDto input,CancellationToken cancellation);
        public Task DeleteAsync(int id, CancellationToken cancellation);
        public Task<User> GetByIdAsync(int id, CancellationToken cancellation);
        public Task<List<UserViewModel>> GetListAsync(string? q, CancellationToken cancellation);
        public Task ToggleActivationAsync(int id, UpdateUserDto input, CancellationToken cancellation);
    }
}
