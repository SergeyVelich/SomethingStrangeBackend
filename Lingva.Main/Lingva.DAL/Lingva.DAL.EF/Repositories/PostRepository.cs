using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Repositories
{
    public class PostRepository : Repository, IPostRepository
    {
        public PostRepository(DictionaryContext dbContext) : base(dbContext)
        {

        }
    }
}
