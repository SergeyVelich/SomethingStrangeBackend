using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.BC.Services;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Moq;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lingva.BC.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class PostManagerTests
    {
        private readonly List<Post> _postList;
        private readonly List<PostDto> _postListDto;
        private IPostManager _postManager;
        private readonly Mock<IPostRepository> _repoMock;
        private readonly Mock<IDataAdapter> _dataAdapter;

        public PostManagerTests()
        {
            _postList = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Title = "Harry Potter",
                    Date = DateTime.Now,
                    PreviewText = "Description",
                    FullText = "Description",
                    LanguageId = 1,
                    Language = new Language
                    {
                        Id = 1,
                        Name = "en",
                    },
                },
                new Post
                {
                    Id = 2,
                    Title = "Librium",
                    Date = DateTime.Now,
                    PreviewText = "Description",
                    FullText = "Description",
                    LanguageId = 2,
                    Language = new Language
                    {
                        Id = 2,
                        Name = "ru",
                    },
                }
            };

            _postListDto = new List<PostDto>
            {
                new PostDto
                {
                    Id = 1,
                    Title = "Harry Potter",
                    Date = DateTime.Now,
                    PreviewText = "Description",
                    FullText = "Description",
                    LanguageId = 1,
                },
                new PostDto
                {
                    Id = 12,
                    Title = "Librium",
                    Date = DateTime.Now,
                    PreviewText = "Description",
                    FullText = "Description",
                    LanguageId = 2
                }
            };           

            _repoMock = new Mock<IPostRepository>();
            _dataAdapter = new Mock<IDataAdapter>();
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);
        }

        [Fact]
        public async Task GetListAsync_ShouldNot_Return_NotNull()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Post>()).Returns(Task.FromResult<IEnumerable<Post>>(_postList));
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);

            //act
            var result = await _postManager.GetListAsync();

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetListAsync_Should_Return_SetValue()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Post>()).Returns(Task.FromResult<IEnumerable<Post>>(_postList));
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);
            _dataAdapter.Setup(d => d.Map<IEnumerable<PostDto>>(_postList)).Returns(_postListDto);

            //act
            var result = await _postManager.GetListAsync();

            //assert
            Assert.True(result.Count() == _postList.Count());
        }

        [Fact]
        public async Task GetListAsyncWithOptions_ShouldNot_Return_NotNull()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Post>()).Returns(Task.FromResult<IEnumerable<Post>>(_postList));
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);
            _dataAdapter.Setup(d => d.Map<IEnumerable<PostDto>>(_postList)).Returns(_postListDto);

            Mock<IQueryOptions> queryOptions = new Mock<IQueryOptions>();
            queryOptions.Setup(q => q.Filters).Returns<Post>(null);
            queryOptions.Setup(q => q.Sorters).Returns<Post>(null);
            queryOptions.Setup(q => q.Includers).Returns<Post>(null);
            queryOptions.Setup(q => q.Pagenator).Returns<Post>(null);

            //act
            var result = await _postManager.GetListAsync(queryOptions.Object);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByValidId_ShouldNot_Return_Null()
        {
            //arrange
            int validId = 1;
            Post post = new Post { Id = validId };
            PostDto postDto = new PostDto { Id = validId };

            _dataAdapter.Setup(d => d.Map<PostDto>(post)).Returns(postDto);
            _repoMock.Setup(r => r.GetByIdAsync<Post>(validId)).Returns(Task.FromResult(post));
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);

            //act
            var order = await _postManager.GetByIdAsync(validId);

            //assert
            Assert.NotNull(order);
        }

        [Fact]
        public async Task GetByInvalidId_Should_Return_Null()
        {
            //arrange
            int invalidId = -1;

            _repoMock.Setup(r => r.GetByIdAsync<Post>(invalidId)).Returns(Task.FromResult<Post>(null));
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);

            //act
            var order = await _postManager.GetByIdAsync(invalidId);

            //assert
            Assert.Null(order);
        }

        [Fact]
        public async Task AddAsync_WasExecute()
        {
            //arrange
            Post post = new Post { Id = 1 };

            _repoMock.Setup(r => r.CreateAsync<Post>(null));
            _dataAdapter.Setup(d => d.Map<Post>(null)).Returns(post);
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);

            //act
            await _postManager.AddAsync(null);

            //assert
            _repoMock.Verify(mock => mock.CreateAsync(post), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_WasExecute()
        {
            //arrange
            Post post = new Post { Id = 1 };
            PostDto postDto = new PostDto { Id = 1 };

            _repoMock.Setup(r => r.GetByIdAsync<Post>(post.Id)).Returns(Task.FromResult(post));
            _repoMock.Setup(r => r.UpdateAsync(post)).Returns(Task.FromResult(post));
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);

            //act
            await _postManager.UpdateAsync(postDto);

            //assert
            _repoMock.Verify(mock => mock.UpdateAsync(post), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_WasExecute()
        {
            //arrange
            Post post = new Post { Id = 1 };

            _repoMock.Setup(r => r.DeleteAsync<Post>(post.Id));
            _dataAdapter.Setup(d => d.Map<Post>(null)).Returns(post);
            _postManager = new PostManager(_repoMock.Object, _dataAdapter.Object);

            //act
            await _postManager.DeleteAsync(post.Id);

            //assert
            _repoMock.Verify(mock => mock.DeleteAsync<Post>(post.Id), Times.Once());
        }
    }
}
