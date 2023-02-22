using AutoMapper;
using Entities.BUSINESS;
using Entities.General;
using Entities.REPOSITORY;
using System;
using System.Collections.Generic;

namespace Business.BusinessProfile
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<sigortali, SigortaliDTO>()
                .ForMember(dest =>dest.ADSOYAD1,opt => opt.MapFrom(src => string.IsNullOrEmpty(src.AD) && string.IsNullOrEmpty(src.SOYAD) ? null : $"{src.AD} {src.SOYAD}"));
            CreateMap<SigortaliDTO, sigortali>();
            CreateMap<ResultModel<sigortali>, MiddlewareResult<SigortaliDTO>>().ReverseMap();
            CreateMap<ResultModel<List<sigortali>>, MiddlewareResult<List<SigortaliDTO>>>().ReverseMap();


            CreateMap<kullanici, KullaniciDTO>().ReverseMap();
            CreateMap<ResultModel<kullanici>, MiddlewareResult<KullaniciDTO>>().ReverseMap();
            CreateMap<ResultModel<List<kullanici>>, MiddlewareResult<List<KullaniciDTO>>>().ReverseMap();

            CreateMap<person, PersonDTO>().ReverseMap();
            CreateMap<ResultModel<person>, MiddlewareResult<PersonDTO>>().ReverseMap();
            CreateMap<ResultModel<List<person>>, MiddlewareResult<List<PersonDTO>>>().ReverseMap();

            CreateMap<Police, PoliceDTO>().ReverseMap();
            CreateMap<ResultModel<Police>, MiddlewareResult<PoliceDTO>>().ReverseMap();
            CreateMap<ResultModel<List<Police>>, MiddlewareResult<List<PoliceDTO>>>().ReverseMap();

            CreateMap<Tarife, TarifeDTO>().ReverseMap();
            CreateMap<ResultModel<Tarife>, MiddlewareResult<TarifeDTO>>().ReverseMap();
            CreateMap<ResultModel<List<Tarife>>, MiddlewareResult<List<TarifeDTO>>>().ReverseMap();

            CreateMap<Zeyl, ZeylDTO>().ReverseMap();
            CreateMap<ResultModel<Zeyl>, MiddlewareResult<ZeylDTO>>().ReverseMap();
            CreateMap<ResultModel<List<Zeyl>>, MiddlewareResult<List<ZeylDTO>>>().ReverseMap();

            CreateMap<Zeyltips, ZeyltipsDTO>().ReverseMap();
            CreateMap<ResultModel<Zeyltips>, MiddlewareResult<ZeyltipsDTO>>().ReverseMap();
            CreateMap<ResultModel<List<Zeyltips>>, MiddlewareResult<List<ZeyltipsDTO>>>().ReverseMap();



        }
    }
    public static class BusinessMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<BusinessProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
}
