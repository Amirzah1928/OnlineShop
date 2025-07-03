using MediatR;
using System.Text.Json;


namespace OnlineShop.Application.Events.User.Changes
{
    public class UserChangeEventLogHandler : INotificationHandler<UserChangeEvent>
    {
        public Task Handle(UserChangeEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"BookChangeEvent received: {JsonSerializer.Serialize(notification)}");
            return Task.CompletedTask;
        }
    }
}
