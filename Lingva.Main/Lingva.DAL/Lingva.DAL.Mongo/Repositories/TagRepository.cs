using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Mongo.Repositories
{
    public class TagRepository : Repository, ITagRepository
    {
        public TagRepository(MongoContext dbContext) : base(dbContext)
        {

        }

        public Task<IEnumerable<Tag>> GetListByPostAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
