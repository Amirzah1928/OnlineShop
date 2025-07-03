using Microsoft.AspNetCore.Mvc;
using OnlineShop.DomainModel.Enums;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.DomainService.Services
{
    public interface IUserService
    {
        public Task<User> CreateAsync(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type, CancellationToken cancellation);
        public Task<User> UpdateAsync(int id, string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type, CancellationToken cancellation);
        public Task<User> DeleteAsync(int id, CancellationToken cancellation);
        public Task<UserViewModel> GetByIdAsync(int id, CancellationToken cancellation);
        public Task<PaginationResult<UserViewModel>> GetListAsync(string? q, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, OrderType? orderType, CancellationToken cancellation);
        public Task<UserViewModel> GetTrackingCodeAsync(string code, [FromRoute] string Prefix, CancellationToken cancellation);
        public Task<User> ToggleActivationAsync(int id, CancellationToken cancellation);
    }
}
