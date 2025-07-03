using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.DomainModel.Enums;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Exceptions;
using OnlineShop.DomainService.Failovers;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.Helper;
using OnlineShop.DomainService.Specifications;
using OnlineShop.DomainService.ViewModels;
using OnlineShop.Resources.Messages;


namespace OnlineShop.DomainService.Services
{
    public class UserService(IUnitOfWork unitOfWork, IReadUnitOfWork readUnitOfWork, IMemoryCache memoryCache, FallbackTrackingCodeProxy fallbackTrackingCodeProxy) : IUserService
    {
        //Write
        public async Task<User> CreateAsync(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type, CancellationToken cancellation)
        {
            var user = User.Create(firstName, lastName, phoneNumber, coordinate, type);

            await unitOfWork.UserRepository.AddAsync(user, cancellation);
            await unitOfWork.CommitAsync(cancellation); 

            return user;
        }




        public async Task<User> UpdateAsync(int id, string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type, CancellationToken cancellation)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id, cancellation)
               ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(User)));


            user.Update(firstName, lastName, phoneNumber, coordinate, type);


            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync(cancellation);

            memoryCache.Remove(id);

            return user;
        }





        public async Task<User> DeleteAsync(int id, CancellationToken cancellation)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id, cancellation)
                ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(User)));

            unitOfWork.UserRepository.Delete(user);
            await unitOfWork.CommitAsync(cancellation);

            memoryCache.Remove(id);

            return user;
        }




        public async Task<User> ToggleActivationAsync(int id, CancellationToken cancellation)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id, cancellation)
               ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(User)));

            user.ToggleActivation();

            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync(cancellation);

            memoryCache.Remove(id);

            return user;
        }


        //Read

        public async Task<UserViewModel> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var user = memoryCache.Get<User>(id);

            if (user is null)
            {
                user = await readUnitOfWork.UserRepository.GetByIdAsync(id, cancellation)
                   ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(User)));

                memoryCache.Set(id, user, DateTime.Now.AddSeconds(30));
            }

            var viewModel = user.ToVieModel();

            return viewModel;
        }






        public async Task<PaginationResult<UserViewModel>> GetListAsync(string? q, [FromQuery] int? pageSize, [FromQuery] int? pageNumber, OrderType? orderType, CancellationToken cancellation)
        {
            var key = $"UserSalt_{q}_{pageSize}_{pageNumber}_{orderType}";
            var cachedUsers = memoryCache.Get<PaginationResult<UserViewModel>>(key);

            if (cachedUsers is null)
            {
                var specification = new GetUsersByTitleContainsSpecification(q, pageSize, pageNumber, orderType);
                var (totalCount, users) = await readUnitOfWork.UserRepository.GetListAsync(specification, cancellation);

                var viewModels = users.ToVieModel();

                var result = new PaginationResult<UserViewModel>
                {
                    PageSize = pageSize ?? 0,
                    PageNumber = pageNumber ?? 0,
                    TotalCount = totalCount,
                    Data = viewModels
                };


                memoryCache.Set(key, result, DateTime.Now.AddSeconds(30));

                return result;
            }

            return cachedUsers;
        }





        public async Task<UserViewModel> GetTrackingCodeAsync(string code, string Prefix, CancellationToken cancellation)
        {
            var key = $"{code}-{Prefix}";
            var user = memoryCache.Get<User>(key);

            if (user is null)
            {
                var specification = new GetUserByTrackingCodeContainsSpecification(code, Prefix);

                user = await unitOfWork.UserRepository.GetByTrackingCodeAsync(specification, cancellation)
                  ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(User)));

                memoryCache.Set(key, user, DateTime.Now.AddSeconds(30));
            }

            var viewModel = user.ToVieModel();

            return viewModel;
        }




        public async Task SetTrackingCode(int id, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.UserRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(User)));

            if (string.IsNullOrEmpty(book.TrackingCode))
            {
                var trackingCodes = await fallbackTrackingCodeProxy.Get(1, cancellationToken);
                book.SetTrackingCode(trackingCodes[0]);

                unitOfWork.UserRepository.Update(book);
                await unitOfWork.CommitAsync(cancellationToken);
            }
        }
    }
}
