
namespace OnlineShop.DomainModel.Models
{
    public class UserOption
    {
        public int Id { get; set; } 
        public string Description { get; private set; }

        private UserOption(string description)
        {
            Description = description;
        }

        public static UserOption Create(string description)
        {
            return new UserOption(description);
        }
    }
}
