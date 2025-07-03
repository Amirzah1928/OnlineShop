using MediatR;
using OnlineShop.DomainModel.Enums;
using OnlineShop.DomainModel.Models;


namespace OnlineShop.Application.Events.User.Changes
{
    public class UserChangeEvent : INotification
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public UserType Type { get; set; }
        public Coordinate Coordinate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public static partial class EventMapper
    {
        public static UserChangeEvent ToEvent(this DomainModel.Models.User user)
        {
            return new UserChangeEvent
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                Type = user.Type,
                Coordinate = user.Coordinate,
                IsDeleted = user.IsDeleted,
            };
        }
    }
}
