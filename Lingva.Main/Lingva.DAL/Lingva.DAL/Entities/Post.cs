using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Post : BaseBE
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }     
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }
        public string PreviewText { get; set; }
        public string FullText { get; set; }

        public virtual Language Language { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
