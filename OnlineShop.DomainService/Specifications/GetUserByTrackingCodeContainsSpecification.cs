using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Features;

namespace OnlineShop.DomainService.Specifications
{
    public class GetUserByTrackingCodeContainsSpecification : BaseSpecification<User>
    {
        public GetUserByTrackingCodeContainsSpecification(string code, string prefix)
        {
            if(!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(prefix))
            {
                AddCriteria(x => x.TrackingCode == $"{prefix}-{code}");
            }
        }
    }
}
