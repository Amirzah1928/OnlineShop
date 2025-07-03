using AutoMapper;
using Humanizer;
using OnlineShop.DomainModel.Attributes;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.DomainService.Helper
{
    public static class MapperHelper
    {
        public static UserViewModel ToVieModel(this User user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            });

            var mapper = new Mapper(config);


            return mapper.Map<UserViewModel>(user);
        }


        public static List<UserViewModel> ToVieModel(this List<User> users)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            });

            var mapper = new Mapper(config);


            return mapper.Map<List<UserViewModel>>(users);
        }



        public static List<EnumViewModel> ToVieModel(this IEnumerable<Enum> enums)
        {
            return enums.Select(enumValue =>
            {
                var userInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

                var infoAttributes = userInfo?
                    .GetCustomAttributes(typeof(InformationAttribute), false)
                    .Cast<InformationAttribute>()
                    .ToList();

                var infoDict = new Dictionary<string, object>();
                if (infoAttributes != null)
                {
                    foreach (var attr in infoAttributes)
                    {
                        infoDict[attr.Key] = attr.Value;
                    }
                }

                return new EnumViewModel
                {
                    Id = (int)(object)enumValue,
                    Type = enumValue.ToString(),
                    Description = enumValue.Humanize(),
                    Information = infoDict
                };
            }).ToList();
        }

    }
}
