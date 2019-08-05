﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class User : BaseBE
    {
        public string Name { get; set; }
        public string Email { get; set; }

       public virtual ICollection<Post> Posts { get; set; }

        public User()
        {
            Posts = new List<Post>();
        }
    }
}
