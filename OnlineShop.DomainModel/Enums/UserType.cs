using OnlineShop.DomainModel.Attributes;
using System.ComponentModel;

namespace OnlineShop.DomainModel.Enums
{
    [EnumEndpoint("UserType")]
    public enum UserType
    {
        [Description("نقره ای")]
        [Information("Rank",2)]
        [Information("Bonus Coin",50)]
        Silver = 1,
        [Description("طلایی")]
        [Information("Rank", 1)]
        [Information("Bonus Coin", 20)]
        Golden = 2
    }
}
