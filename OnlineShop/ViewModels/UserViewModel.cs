using OnlineShop.Models;

namespace OnlineShop.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public bool Isactive { get; set; }
        public required string FullName { get; set; }
        public required Coordinate Coordinate { get; set; }

    }
}
