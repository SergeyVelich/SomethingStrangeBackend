//using Lingva.Additional.Mapping.DataAdapter;
//using Lingva.ASP.Infrastructure.Adapters;
//using Lingva.ASP.Infrastructure.Models;
//using Lingva.BC.Contracts;
//using Lingva.BC.Dto;
//using Lingva.WebAPI.Controllers;
//using Lingva.WebAPI.Models.Entities;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Logging;
//using NSubstitute;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Threading.Tasks;

//namespace Lingva.WebAPI.Tests
//{
//    [ExcludeFromCodeCoverage]
//    public class PostControllerTests
//    {
//        private List<PostDto> _postDtoList;
//        private PostController _postController;
//        private IPostManager _postManager;
//        private IDataAdapter _dataAdapter;
//        private ILogger<PostController> _logger;
//        private QueryOptionsAdapter _queryOptionsAdapter;
//        private PostsListOptionsModel _optionsModel;
//        private IFileStorageManager _fileStorageManager;
//        private IHostingEnvironment _appEnvironment;

//        [SetUp]
//        public void Setup()
//        {
//            _appEnvironment = Substitute.For<IHostingEnvironment>();
//            _postManager = Substitute.For<IPostManager>();
//            _fileStorageManager = Substitute.For<IFileStorageManager>();
//            _dataAdapter = Substitute.For<IDataAdapter>();          
//            _logger = Substitute.For<ILogger<PostController>>();
//            _queryOptionsAdapter = Substitute.For<QueryOptionsAdapter>();
//            _optionsModel = Substitute.For<PostsListOptionsModel>();

//            _postDtoList = new List<PostDto>
//            {
//                new PostDto
//                {
//                    Id = 1,
//                    Name = "Harry Potter",
//                    Date = DateTime.Now,
//                    Description = "Description",
//                    ImagePath = "Picture",
//                    LanguageId = 1
//                },
//                new PostDto
//                {
//                    Id = 12,
//                    Name = "Librium",
//                    Date = DateTime.Now,
//                    Description = "Description",
//                    ImagePath = "Picture",
//                    LanguageId = 2
//                }
//            };
//        }

//        [Test]
//        public async Task Index_Get_NotNull()
//        {
//            //arrange
//            _postManager.GetListAsync().Returns(_postDtoList);
//            _postController = new PostController(_appEnvironment, _postManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _postController.Index(_optionsModel);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Get_Get_NotNull()
//        {
//            //arrange
//            int validId = 1;
//            PostViewModel postViewModel = new PostViewModel { Id = validId };
//            PostDto postDto = new PostDto { Id = validId };

//            _dataAdapter.Map<PostDto>(postViewModel).Returns(postDto);
//            _postManager.GetByIdAsync(validId).Returns(postDto);
//            _postController = new PostController(_appEnvironment, _postManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _postController.Get(1);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Create_Post_NotNull()
//        {
//            //arrange
//            PostViewModel postViewModel = new PostViewModel { Id = 1 };
//            PostDto postDto = new PostDto { Id = 1 };

//            _postManager.AddAsync(Arg.Any<PostDto>()).Returns(Task.FromResult(postDto));
//            _dataAdapter.Map<PostDto>(postViewModel).Returns(postDto);
//            _dataAdapter.Map<PostViewModel>(postDto).Returns(postViewModel);
//            _postController = new PostController(_appEnvironment, _postManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _postController.Create(postViewModel);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Update_Post_NotNull()
//        {
//            //arrange
//            PostViewModel postViewModel = new PostViewModel { Id = 1 };
//            PostDto postDto = new PostDto { Id = 1 };

//            _postManager.UpdateAsync(Arg.Any<PostDto>()).Returns(postDto);
//            _dataAdapter.Map<PostDto>(postViewModel).Returns(postDto);
//            _dataAdapter.Map<PostViewModel>(postDto).Returns(postViewModel);
//            _postController = new PostController(_appEnvironment, _postManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _postController.Create(postViewModel);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Delete_NotNull()
//        {
//            //arrange
//            PostViewModel postViewModel = new PostViewModel { Id = 1 };
//            PostDto postDto = new PostDto { Id = 1 };

//            _postManager.DeleteAsync(Arg.Any<int>()).Returns(Task.FromResult(postDto));
//            _dataAdapter.Map<PostDto>(postViewModel).Returns(postDto);
//            _dataAdapter.Map<PostViewModel>(postDto).Returns(postViewModel);
//            _postController = new PostController(_appEnvironment, _postManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _postController.Delete(postViewModel.Id);

//            //assert
//            Assert.NotNull(result);
//        }
//    }
//}
