

namespace OnlineShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public bool Isactive { get; set; }
        public required Coordinate Coordinate { get; set; }
    }
}
