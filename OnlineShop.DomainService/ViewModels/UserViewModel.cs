using OnlineShop.DomainModel.Models;

namespace OnlineShop.DomainService.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public required string FullName { get; set; }
        public required Coordinate Coordinate { get; set; }
        public required string TrackingCode { get; set; }
        public List<UserOption> Options { get; set; } = [];
        public List<UserTag> Tags { get; set; } = [];
    }
}
