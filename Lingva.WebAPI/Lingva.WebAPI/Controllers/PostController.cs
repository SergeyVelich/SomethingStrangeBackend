using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Infrastructure.Adapters;
using Lingva.WebAPI.Infrastructure.Models;
using Lingva.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    //[Authorize(Policy = "ApiReader")]
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostManager _postManager;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IDataAdapter _dataAdapter;
        private readonly QueryOptionsAdapter _queryOptionsAdapter;
        private readonly ILogger<PostController> _logger;        

        public PostController(IPostManager postManager, IFileStorageManager fileStorageManager, IDataAdapter dataAdapter, QueryOptionsAdapter queryOptionsAdapter, ILogger<PostController> logger)
        {
            _postManager = postManager;
            _fileStorageManager = fileStorageManager;
            _dataAdapter = dataAdapter;
            _queryOptionsAdapter = queryOptionsAdapter;
            _logger = logger;
        }

        // GET: api/post
        //[Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PostsListOptionsModel postsListOptionsModel)
        {
            IEnumerable<PostDto> postsDto = await _postManager.GetListAsync(); //глючит в автомаппере
            //IEnumerable<PostDto> postsDto = await _postManager.GetListAsync(_queryOptionsAdapter.Map(postsListOptionsModel));

            return Ok(_dataAdapter.Map<IEnumerable<PostViewModel>>(postsDto));
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] PostsListOptionsModel postsListOptionsModel)
        {
            int pageTotal = await _postManager.CountAsync(_queryOptionsAdapter.Map(postsListOptionsModel));

            return Ok(pageTotal);
        }

        // GET: api/post/get?id=1
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            PostDto postDto = await _postManager.GetByIdAsync(id);

            if (postDto == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<PostViewModel>(postDto));
        }

        // POST: api/post/create
        /// <summary>
        /// Creates a Post.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "Name": "Harry Potter",
        ///        "Date": 12.10.2019,
        ///        "LanguageId": 2,
        ///        "Description": "3",
        ///        "Picture": "2"
        ///     }
        ///
        /// </remarks>
        [HttpPost("create")]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PostDto postDto = _dataAdapter.Map<PostDto>(postViewModel);
            await _postManager.AddAsync(postDto);

            return Ok("Saved successful");
        }

        // PUT: api/post/update
        [HttpPut("update")]
        public async Task<IActionResult> Update(PostViewModel postViewModel)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PostDto postDto = _dataAdapter.Map<PostDto>(postViewModel);
            await _postManager.UpdateAsync(postDto);

            return Ok("Updated successful");
        }

        // DELETE: api/post/delete?id=1
        /// <summary>
        /// Deletes a specific Post.
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _postManager.DeleteAsync(id);

            return Ok("Deleted successful");
        }

        // GET: api/post/getImage?fileId=1
        [AllowAnonymous]
        [HttpGet("getImage")]
        public async Task<IActionResult> GetImage([FromQuery] int id)
        {
            IActionResult result;

            string folderName = Path.Combine("Resources", "Images");
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            FileInfo[] files = directory.GetFiles(id.ToString() + ".*");
            string path = files.Select(file => file.FullName).FirstOrDefault();
            if(path == null)
            {
                result = NoContent();
            }
            else
            {
                FileStream image = System.IO.File.OpenRead(path);
                result = File(image, "image/jpeg");
            }

            return result;
        }

        private PostViewModel GetPostViewModelFromRequest()
        {
            PostViewModel postViewModel = null;
            var stringItems = Request.Form["post"];
            foreach (var stringItem in stringItems)
            {
                postViewModel = JsonConvert.DeserializeObject<PostViewModel>(stringItem);
            }

            return postViewModel;
        }
        private async Task<string> SaveFileFromRequestAsync(int id)
        {
            string path = null;
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                string folderName = Path.Combine("Resources", "Images");
                path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), folderName), id.ToString());
                await _fileStorageManager.SaveFileAsync(file, path, FileMode.Create);
            }

            return path;
        }
    }
}