namespace OnlineShop.DTOs
{
    public class GetUsersWithFullNameDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public bool Isactive { get; set; }
        public string FullName { get; set; }
    }
}
