namespace OnlineShop.DomainModel.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public bool IsDeleted { get; private set; }


        public void Created()
        {
            CreatedAt = DateTime.Now;
        }


        public void Updated()
        {
            UpdatedAt = DateTime.Now;
        }


        public void Deleted()
        {
            UpdatedAt = DateTime.Now;
            IsDeleted = true;
        }
    }
}
