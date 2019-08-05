using Lingva.DAL.Repositories;

namespace Lingva.DAL.CosmosSqlApi.Repositories
{
    public class PostRepository : Repository, IPostRepository
    {
        public PostRepository(CosmosSqlApiContext dbContext) : base(dbContext)
        {

        }
    }
}
