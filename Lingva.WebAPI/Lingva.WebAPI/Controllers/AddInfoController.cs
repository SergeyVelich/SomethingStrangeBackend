using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/info")]
    [ApiController]
    public class AddInfoController : ControllerBase
    {
        private readonly IAddInfoManager _addInfoManager;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<AddInfoController> _logger;

        public AddInfoController(IAddInfoManager addInfoManager, IDataAdapter dataAdapter, ILogger<AddInfoController> logger)
        {
            _addInfoManager = addInfoManager;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        // GET: api/info/languages
        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguagesList()
        {
            IEnumerable<LanguageDto> languages = await _addInfoManager.GetLanguagesListAsync();

            return Ok(_dataAdapter.Map<IEnumerable<LanguageViewModel>>(languages));
        }
    }
}