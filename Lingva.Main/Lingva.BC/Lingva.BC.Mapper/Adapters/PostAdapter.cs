using AutoMapper;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Mapper.Adapters
{
    [ExcludeFromCodeCoverage]
    public class PostAdapter : Profile
    {
        public PostAdapter()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dto => dto.LanguageId, opt => opt.MapFrom(g => g.Language == null ? 0 : g.Language.Id))
                .ForMember(dto => dto.LanguageName, opt => opt.MapFrom(g => g.Language == null ? null : g.Language.Name))
                .ForMember(dto => dto.AuthorId, opt => opt.MapFrom(g => g.Author == null ? 0 : g.Author.Id))
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(g => g.Author == null ? null : g.Author.Name));
            CreateMap<PostDto, Post>();
                //.ForMember(dto => dto.Language, opt => opt.MapFrom(g => new Language { Id = g.LanguageId, Name = g.LanguageName }));

            CreateMap<Post, Post>();
        }
    }
}
