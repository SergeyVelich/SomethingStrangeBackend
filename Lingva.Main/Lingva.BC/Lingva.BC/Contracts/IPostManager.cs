using Lingva.BC.Dto;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IPostManager
    {
        Task<IEnumerable<PostDto>> GetListAsync();
        Task<IEnumerable<PostDto>> GetListAsync(IQueryOptions queryOptions);
        Task<int> CountAsync(IQueryOptions queryOptions);

        Task<PostDto> GetByIdAsync(int id);

        Task<PostDto> AddAsync(PostDto postDto);

        Task<PostDto> UpdateAsync(PostDto postDto);

        Task DeleteAsync(int id);
    }
}
