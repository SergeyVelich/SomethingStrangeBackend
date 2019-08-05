using Lingva.DAL.Repositories;

namespace Lingva.DAL.Dapper.Repositories
{
    public class PostRepository : Repository, IPostRepository
    {
        public PostRepository(DapperContext dbContext) : base(dbContext)
        {

        }
    }
}
