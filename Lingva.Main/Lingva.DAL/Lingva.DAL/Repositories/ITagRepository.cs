using Lingva.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories
{
    public interface ITagRepository : IRepository
    {
        Task<IEnumerable<Tag>> GetListByPostAsync(int id);
    }
}
