using AutoMapper;
using Entities.API;
using Entities.BUSINESS;

namespace The_Case2.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoginInput, kullanici>()
                .ForMember(dest => dest.KULLANICIADI, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.SIFRE, opt => opt.MapFrom(src => src.Password));
        }
    }
}
