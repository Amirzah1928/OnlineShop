namespace OnlineShop.DomainService.ViewModels
{
    public class EnumViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<string, object> Information { get; set; }
    }
}
