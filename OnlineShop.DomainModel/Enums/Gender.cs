using OnlineShop.DomainModel.Attributes;
using System.ComponentModel;

namespace OnlineShop.DomainModel.Enums
{
    [EnumEndpoint("Gender")]
    public enum Gender
    {
        [Description("مرد")]
        Male = 1,
        [Description("زن")]
        Female = 2
    }
}
