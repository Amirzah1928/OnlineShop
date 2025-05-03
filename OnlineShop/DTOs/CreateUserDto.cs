using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.DTOs
{
    public class CreateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required Coordinate Coordinate { get; set; }
    }
}
