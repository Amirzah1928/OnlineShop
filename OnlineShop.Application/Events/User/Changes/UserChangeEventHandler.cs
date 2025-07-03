using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.DomainService.Data;

namespace OnlineShop.Application.Events.User.Changes
{
    public class UserChangeEventHandler(IServiceScopeFactory serviceScopeFactory) : INotificationHandler<UserChangeEvent>
    {
        public async Task Handle(UserChangeEvent notification, CancellationToken cancellationToken)
        {
            var scope = serviceScopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IReadUnitOfWork>();

            var user = await unitOfWork.UserRepository.GetByIdAsync(notification.Id, cancellationToken);
            if (user is null) // Create
            {
                user = DomainModel.Models.User.Create(notification.Id, notification.FirstName,notification.LastName,notification.PhoneNumber,notification.Coordinate, notification.Type);

                await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
            }
            else
            { 
                if (!notification.IsDeleted) // Update
                {
                    user.Update(notification.FirstName, notification.LastName,notification.PhoneNumber, notification.Coordinate, notification.Type);

                    unitOfWork.UserRepository.Update(user);
                }
                else // Delete
                {
                    unitOfWork.UserRepository.Delete(user);
                }
            }

            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
