using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Repositories
{
    public class TagRepository : Repository, ITagRepository
    {
        public TagRepository(DictionaryContext dbContext) : base(dbContext)
        {

        }

        public virtual async Task<IEnumerable<Tag>> GetListByPostAsync(int id)
        {
            return await _dbContext.Set<PostTag>().Where(gu => gu.PostId == id).Include(gu => gu.Tag).Select(gu => gu.Tag).ToListAsync();
        }
    }
}
