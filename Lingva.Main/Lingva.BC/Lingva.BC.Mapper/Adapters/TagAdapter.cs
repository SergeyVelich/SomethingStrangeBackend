using AutoMapper;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class TagAdapter : Profile
    {
        public TagAdapter()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<TagDto, Tag>();

            CreateMap<Tag, Tag>();
        }
    }
}
