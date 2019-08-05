using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Dto
{
    public class PostDto
    {
        [ExcludeFromCodeCoverage]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string PreviewText { get; set; }
        public string FullText { get; set; }
    }
}
