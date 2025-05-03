using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Data;
using OnlineShop.DTOs;
using OnlineShop.Exceptions;
using OnlineShop.Models;
using OnlineShop.Repositories;
using OnlineShop.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineShop.Services
{
    public class UserService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IUserService
    {
        public async Task CreateAsync(CreateUserDto newUser, CancellationToken cancellation)
        {

            if (newUser.PhoneNumber.Length != 11)
                throw new BadRequestException("Invalid PhoneNumber");

            if (!newUser.PhoneNumber.StartsWith("09"))
                throw new BadRequestException("Invalid PhoneNumber");

            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber,
                Isactive = true,
                Coordinate = newUser.Coordinate,
            };

            await unitOfWork.userRepository.AddAsync(user, cancellation);
            await unitOfWork.CommitAsync(cancellation);

        }

        public async Task UpdateAsync(int id, UpdateUserDto input, CancellationToken cancellation)
        {
            var user = await unitOfWork.userRepository.GetByIdAsync(id, cancellation);
            if (user == null)
                throw new NotFoundException("Not Found");

            if (!string.IsNullOrEmpty(input.FirstName))
                user.FirstName = input.FirstName;

            if (!string.IsNullOrEmpty(input.LastName))
                user.LastName = input.LastName;

            if (!string.IsNullOrEmpty(input.PhoneNumber))
            {
                if (input.PhoneNumber.Length != 11)
                    throw new BadRequestException("Invalid PhoneNumber");
                if (!input.PhoneNumber.StartsWith("09"))
                    throw new BadRequestException("Invalid PhoneNumber");
                user.PhoneNumber = input.PhoneNumber;
            }

            user.Coordinate = input.Coordinate;

            unitOfWork.userRepository.Update(user);
            await unitOfWork.CommitAsync(cancellation);

            memoryCache.Remove(id);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellation)
        {
            var user = await unitOfWork.userRepository.GetByIdAsync(id, cancellation);
            if (user == null)
                throw new NotFoundException("Not Found");

            unitOfWork.userRepository.Delete(user);
            await unitOfWork.CommitAsync(cancellation);

            memoryCache.Remove(id);
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var user = memoryCache.Get<User>(id);

            if (user is null)
            {
                user = await unitOfWork.userRepository.GetByIdAsync(id, cancellation);
                if (user is null)
                    throw new NotFoundException("User Not Found");

                memoryCache.Set(id, user, DateTime.Now.AddSeconds(30));
            }

            return user;
        }

        public async Task<List<UserViewModel>> GetListAsync(string? q, CancellationToken cancellation)
        {
            var key = $"UserSalt_{q}";
            var users = memoryCache.Get<List<UserViewModel>>(key);
            List<UserViewModel> viewModel;

            if (users is null)
            {
                var query = await unitOfWork.userRepository.GetListAsync(q, cancellation);

                viewModel = query.Select(x => new UserViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Isactive = x.Isactive,
                    FullName = x.FirstName + " " + x.LastName,
                    Coordinate = x.Coordinate,
                }).ToList();

                memoryCache.Set(key, viewModel, DateTime.Now.AddSeconds(30));

                return viewModel;
            }

           return users;
        }

        public async Task ToggleActivationAsync(int id, UpdateUserDto input, CancellationToken cancellation)
        {
            var user = await unitOfWork.userRepository.GetByIdAsync(id, cancellation);
            if (user == null)
                throw new NotFoundException("Not Found");
            user.Isactive = !user.Isactive;


            unitOfWork.userRepository.Update(user);
            await unitOfWork.CommitAsync(cancellation);

            memoryCache.Remove(id);
        }


    }
}
