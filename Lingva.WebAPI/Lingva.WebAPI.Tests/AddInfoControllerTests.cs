using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Tests
{
    [ExcludeFromCodeCoverage]
    public class AddInfoControllerTests
    {
        private List<LanguageDto> _languageDtoList;
        private AddInfoController _infoController;
        private IAddInfoManager _addInfoManager;
        private IDataAdapter _dataAdapter;
        private ILogger<AddInfoController> _logger;

        [SetUp]
        public void Setup()
        {
            _addInfoManager = Substitute.For<IAddInfoManager>();
            _dataAdapter = Substitute.For<IDataAdapter>();          
            _logger = Substitute.For<ILogger<AddInfoController>>();

            _languageDtoList = new List<LanguageDto>
            {
                new LanguageDto
                {
                    Id = 1,
                    Name = "en",
                },
                new LanguageDto
                {
                    Id = 2,
                    Name = "ru",
                }
            };
        }

        [Test]
        public async Task Index_Get_NotNull()
        {
            //arrange
            _addInfoManager.GetLanguagesListAsync().Returns(_languageDtoList);
            _infoController = new AddInfoController(_addInfoManager, _dataAdapter, _logger);

            //act
            var result = await _infoController.GetLanguagesList();

            //assert
            Assert.NotNull(result);
        }
    }
}
