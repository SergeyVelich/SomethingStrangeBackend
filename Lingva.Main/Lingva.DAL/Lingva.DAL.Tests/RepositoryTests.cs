using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Tests
{
    [ExcludeFromCodeCoverage]
    public class RepositoryTests
    {
        private IPostRepository _postRepository;
        private List<Post> _postList;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DictionaryContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Lingva;Trusted_Connection=True;MultipleActiveResultSets=true;");
            var _dbContext = new DictionaryContext(optionsBuilder.Options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _postList = new List<Post>()
            {
                new Group
                {
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    LanguageId = 1,
                },
                new Group
                {
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    LanguageId = 1,
                }
            };
            _dbContext.Set<Post>().AddRange(_postList);
            _dbContext.SaveChanges();

            _postRepository = new PostRepository(_dbContext);
        }

        [Test]
        public async Task GetListAsync_ShouldNot_ReturnNull()
        {
            var orders = await _postRepository.GetListAsync<Post>();

            Assert.NotNull(orders);
        }

        [Test]
        public async Task GetByIdAsync_ValidId_ReturnGroup()
        {
            var posts = await _postRepository.GetListAsync<Post>();
            int validId = posts.First().Id;
            var result = await _postRepository.GetByIdAsync<Post>(validId);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetByIdAsync_InvalidId_ReturnNull()
        {
            int invalidId = -1;
            var result = await _postRepository.GetByIdAsync<Post>(invalidId);

            Assert.Null(result);
        }

        [Test]
        public async Task AddAsync_Group_ReturnGroup()
        {
            var result = await _postRepository.CreateAsync(_postList[0]);

            Assert.True(result.Id != 0);
        }

        [Test]
        public async Task UpdateAsync_Group_ReturnGroup()
        {
            var groupFromDb = await _postRepository.GetByIdAsync<Post>(1);
            int languageId = groupFromDb.LanguageId;
            groupFromDb.LanguageId ++;
            var result = await _postRepository.UpdateAsync(groupFromDb);

            Assert.True(result.LanguageId != languageId);
        }

        [Test]
        public async Task DeleteAsync_Group_ReturnGroup()
        {
            var groupFromDb = await _postRepository.GetByIdAsync<Post>(2);
            await _postRepository.DeleteAsync<Post>(groupFromDb.Id);
            var result = await _postRepository.GetByIdAsync<Post>(groupFromDb.Id);

            Assert.Null(result);
        }

        //[Fact]
        //public async Task GetListAsync_ShouldNot_Return_NotNull()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);

        //    //act
        //    var result = await _repository.GetListAsync<Post>();

        //    //assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetListAsync_Should_Return_SetValue()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);

        //    //act
        //    var result = await _repository.GetListAsync<Post>();

        //    //assert
        //    Assert.True(result.Count() == _postList.Count());
        //}

        //[Fact]
        //public async Task GetListAsyncWithOptions_ShouldNot_Return_NotNull()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);


        //    //_repoMock.Setup(r => r.GetListAsync<Post>()).Returns(Task.FromResult<IEnumerable<Post>>(_postList));
        //    //_postManager = new GroupService(_repoMock.Object, _data.Object);
        //    //_data.Setup(d => d.Map<IEnumerable<PostDto>>(_postList)).Returns(_postListDto);

        //    var queryOptions = Substitute.For<IQueryOptions>();
        //    //queryOptions.GetFiltersExpression<Post>().Returns();
        //    //queryOptions.GetFiltersExpression<Post>().Returns<Post>(null);
        //    //queryOptions.GetSortersCollection<Post>().Returns<Post>(null);
        //    //queryOptions.GetIncludersCollection<Post>().Returns<Post>(null);
        //    //queryOptions.Pagenator.Returns<Post>(null);

        //    //act
        //    var result = await _repository.GetListAsync<Post>(queryOptions);

        //    //assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetBy_Id_Should_Return_Correct_Object()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);

        //    var products = await _repository.GetListAsync<Post>();
        //    var testProduct = await _repository.GetByIdAsync<Post>(products.First().Id);

        //    Assert.NotNull(testProduct);
        //}

        //[Fact]
        //public async Task GetBy_WrongId_Should_ReturnNull()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);

        //    int badId = -1;
        //    var testProduct = await _repository.GetByIdAsync<Post>(badId);

        //    Assert.Null(testProduct);
        //}

        //[Fact]
        //public async Task Add_Should_Return_Correct_Object()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);

        //    var testPerson = await _repository.CreateAsync(_group);
        //    await _repository.DeleteAsync(testPerson);

        //    Assert.True(_group.CreateDate == testPerson.CreateDate);
        //}

        ////[Fact]
        ////public async Task Remove()
        ////{
        ////    //arrange
        ////    IPostRepository _repository = new Repository(_context);

        ////    var tempPerson = await _repository.CreateAsync(_group);
        ////    await _repository.DeleteAsync(tempPerson);
        ////    var testPerson = await _repository.GetByIdAsync<Post>(tempPerson.Id);

        ////    Assert.Null(testPerson);
        ////}

        //[Fact]
        //public async Task AddAsync_WasExecute()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.CreateAsync<Post>(null));
        //    //_data.Setup(d => d.Map<Post>(null)).Returns(group);
        //    //_postManager = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.CreateAsync<Post>(null);

        //    //assert
        //    //_context.Verify(mock => mock.CreateAsync(group), Times.Once());
        //    //_context.DidNotReceive()..Execute();
        //}

        //[Fact]
        //public async Task UpdateAsync_WasExecute()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.GetByIdAsync<Post>(group.Id)).Returns(Task.FromResult(group));
        //    //_repoMock.Setup(r => r.UpdateAsync(group)).Returns(Task.FromResult(group));
        //    //_postManager = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.UpdateAsync(group);

        //    //assert
        //    //_context.Verify(mock => mock.UpdateAsync(group), Times.Once());
        //}

        //[Fact]
        //public async Task DeleteAsync_WasExecute()
        //{
        //    //arrange
        //    IPostRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.DeleteAsync<Post>(null));
        //    //_data.Setup(d => d.Map<Post>(null)).Returns(group);
        //    //_postManager = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.DeleteAsync(group);

        //    //assert
        //    //_context.Verify(mock => mock.DeleteAsync(group), Times.Once());
        //}
    }
}
