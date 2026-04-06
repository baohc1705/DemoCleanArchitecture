using AutoMapper;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Entities;
using DemoCleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Common.Mappings
{
    public class ApplicationMappingsProfile : Profile
    {
        public ApplicationMappingsProfile()
        {
            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.NewsCount, opt => opt.MapFrom(src => src.News.Count(n => !n.IsDeleted)))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ReverseMap()
                ;
            CreateMap<Menu, MenuShortDto>().ReverseMap();

            CreateMap<News, NewsDto>()
                .ForMember(dest => dest.MenuShort, opt => opt.MapFrom(src => src.Menu))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                ;
            CreateMap<News, NewsShortDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }
    }
}
