using AutoMapper;
using DemoCleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile() 
        {
            CreateMap<Data.PersistenceModels.Menu, Domain.Entities.Menu>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.InverseParent))
                .ForMember(dest => dest.News, opt => opt.MapFrom(src => src.News))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src =>src.Parent))
                ;
                ;
            CreateMap<Data.PersistenceModels.News, Domain.Entities.News>()
                .ForMember(dest => dest.Menu, opt => opt.MapFrom(src => src.Menu))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<NewsStatus>(src.Status)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }
    }
}
