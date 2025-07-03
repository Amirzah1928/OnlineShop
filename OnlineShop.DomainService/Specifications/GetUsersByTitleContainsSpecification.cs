using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;

namespace OnlineShop.DomainService.Specifications
{
    public class GetUsersByTitleContainsSpecification : BaseSpecification<User>
    {
        public GetUsersByTitleContainsSpecification(string? q, int? pageSize, int? pageNumber, OrderType? orderType)
        {
            AddCriteria(x => x.IsActive);

            if (!string.IsNullOrEmpty(q))
            {
                AddCriteria(x => x.FirstName.Contains(q) || x.LastName.Contains(q));
            }

            if (orderType.HasValue)
            {
                AddOrderBy(x => x.Id, orderType.Value);
            }

            if (pageSize.Value != 0 && pageNumber.Value != 0)
            {
                AddPagination(pageSize.Value, pageNumber.Value);
            }
        }
    }
}
