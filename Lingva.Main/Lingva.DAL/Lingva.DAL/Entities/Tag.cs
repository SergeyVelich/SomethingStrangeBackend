using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Tag : BaseBE
    {
        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
