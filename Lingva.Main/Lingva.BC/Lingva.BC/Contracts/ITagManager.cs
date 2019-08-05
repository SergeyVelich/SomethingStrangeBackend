using Lingva.BC.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface ITagManager
    {
        Task<IEnumerable<TagDto>> GetListByPostAsync(int id);
    }
}
