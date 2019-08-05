using AutoMapper;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class PostAdapter : Profile
    {
        public PostAdapter()
        {
            CreateMap<PostDto, PostViewModel>()
                .ForMember(dto => dto.Date, opt => opt.MapFrom(g => g.Date.ToLocalTime()));

            CreateMap<PostViewModel, PostDto>()
                .ForMember(dto => dto.Date, opt => opt.MapFrom(g => g.Date.ToUniversalTime())); ;
        }
    }
}
