using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postRepository;
        private readonly IDataAdapter _dataAdapter;
           
        public PostManager(IPostRepository postRepository, IDataAdapter dataAdapter)
        {
            _postRepository = postRepository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<PostDto>> GetListAsync()
        {
            IEnumerable<Post> posts = await _postRepository.GetListAsync<Post>();

            return _dataAdapter.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<IEnumerable<PostDto>> GetListAsync(IQueryOptions queryOptions)
        {
            IEnumerable<Post> posts = await _postRepository.GetListAsync<Post>(queryOptions);

            return _dataAdapter.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<int> CountAsync(IQueryOptions queryOptions)
        {
            return await _postRepository.CountAsync<Post>(queryOptions);
        }

        public async Task<PostDto> GetByIdAsync(int id)
        {
            Post post = await _postRepository.GetByIdAsync<Post>(id);
            return _dataAdapter.Map<PostDto>(post);
        }

        public async Task<PostDto> AddAsync(PostDto postDto)
        {
            Post post = _dataAdapter.Map<Post>(postDto);
            await _postRepository.CreateAsync(post);

            return _dataAdapter.Map<PostDto>(post);
        }

        public async Task<PostDto> UpdateAsync(PostDto postDto)
        {
            Post currentPost = await _postRepository.GetByIdAsync<Post>(postDto.Id);
            Post updatePost = _dataAdapter.Map<Post>(postDto);
            _dataAdapter.Update(updatePost, currentPost);
            await _postRepository.UpdateAsync(currentPost);

            return _dataAdapter.Map<PostDto>(currentPost);
        }

        public async Task DeleteAsync(int id)
        {
            await _postRepository.DeleteAsync<Post>(id);
        }
    }
}

