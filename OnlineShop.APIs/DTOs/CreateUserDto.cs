using OnlineShop.DomainModel.Enums;
using OnlineShop.DomainModel.Models;

namespace OnlineShop.APIs.DTOs
{
    public class CreateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required Coordinate Coordinate { get; set; }
        public UserType Type { get; set; }

    }
}
