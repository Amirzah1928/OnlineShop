namespace OnlineShop.DomainService.Features
{
    public class PaginationResult<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; } = [];
    }
}
