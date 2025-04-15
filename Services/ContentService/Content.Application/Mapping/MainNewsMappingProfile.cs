using AutoMapper;
using Content.Contracts.Dto;
using Content.Domain.Entities.News;

namespace Content.Application.Mapping
{
    public sealed class MainNewsMappingProfile : Profile
    {
        public MainNewsMappingProfile()
        {
            CreateMap<NewsMain, NewsMainDto>()
                .ForMember(d => d.Id, m => m.MapFrom(e => e.Id))
                .ForMember(d => d.NewsText, m => m.MapFrom(e => e.NewsText))
                .ForMember(d => d.NewsTitle, m => m.MapFrom(e => e.NewsTitle));
        }
    }

}