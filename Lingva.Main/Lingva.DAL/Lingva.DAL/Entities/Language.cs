﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Language : BaseBE
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Language()
        {
            Posts = new List<Post>();
        }
    }
}
