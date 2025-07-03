using OnlineShop.Application.Events.User.Changes;

namespace OnlineShop.Shared.Mapper
{
    public static class UserEventMapper
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
