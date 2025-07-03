using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;

namespace OnlineShop.DomainService.Specifications
{
    public class GetUserWithNoTrackinCodeSpecification : BaseSpecification<User>
    {
        public GetUserWithNoTrackinCodeSpecification()
        {
            AddCriteria(x => string.IsNullOrEmpty(x.TrackingCode));
        }
    }
}
