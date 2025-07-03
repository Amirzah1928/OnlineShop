using OnlineShop.DomainModel.Enums;

namespace OnlineShop.DomainModel.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Coordinate Coordinate { get; set; }
        public UserType Type { get; set; }
        public string TrackingCode { get; set; } = string.Empty;
        public List<UserOption> Options { get; private set; } = [];
        public List<UserTag> Tags { get; private set; } = [];



        private User(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            SetUser(firstName, lastName, phoneNumber, coordinate, type);
            SetIsActive(true);
        }

        private User(int id, string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            SetUser(id, firstName, lastName, phoneNumber, coordinate, type);
            SetIsActive(true);
        }



        public static User Create(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            return new User(firstName, lastName, phoneNumber, coordinate, type);
        }

        public static User Create(int id, string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            return new User(id, firstName, lastName, phoneNumber, coordinate, type);
        }





        public void Update(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            SetUser(firstName, lastName, phoneNumber, coordinate, type);
        }

        public void Update(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type, bool isActive)
        {
            SetUser(firstName, lastName, phoneNumber, coordinate, type);
            SetIsActive(isActive);
        }


        public void ToggleActivation()
        {
            SetIsActive(!IsActive);
        }


        private void SetUser(string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Coordinate = coordinate;
            Type = type;
        }

        private void SetUser(int id,string firstName, string lastName, string phoneNumber, Coordinate coordinate, UserType type)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Coordinate = coordinate;
            Type = type;
        }

        private void SetIsActive(bool IsActive)
        {
            this.IsActive = IsActive;
        }


        public void SetTrackingCode(string trackingCode)
        {
            TrackingCode = trackingCode;
        }


        public void AddOption(string description)
        {
            var option = UserOption.Create(description);
            Options.Add(option);
        }

        public void RemoveOption(int optionId)
        {
            var option = Options.FirstOrDefault(x => x.Id == optionId);
            if (option != null)
            {
                Options.Remove(option);
            }
        }

        public void AddTag(string title, int priority)
        {
            var tag = UserTag.Create(title, priority);
            Tags.Add(tag);
        }

        public void RemoveTag(string title, int priority)
        {
            var tag = UserTag.Create(title, priority);
            Tags.Remove(tag);
        }
    }
}

