using Lingva.BC.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    //[Authorize(Policy = "ApiReader")]
    [Route("api/storage")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly ILogger<PostController> _logger;        

        public FileStorageController(IFileStorageManager fileStorageManager, ILogger<PostController> logger)
        {
            _fileStorageManager = fileStorageManager;
            _logger = logger;
        }

        // GET: api/storage/postimage/1
        [AllowAnonymous]
        [HttpGet("postimage")]
        public async Task<IActionResult> GetPostImage([FromRoute] int id)
        {

            IActionResult result = await GetImage(id);

            return result;
        }

        // GET: api/storage/avatar/1
        [AllowAnonymous]
        [HttpGet("avatar")]
        public async Task<IActionResult> GetAvatar([FromRoute] int id)
        {

            IActionResult result = await GetImage(id);

            return result;
        }

        // POST: api/storage/create
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
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

        private async Task<IActionResult> GetImage(int id)
        {
            IActionResult result;

            string folderName = Path.Combine("Resources", "Images");
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            FileInfo[] files = directory.GetFiles(id.ToString() + ".*");
            string path = files.Select(file => file.FullName).FirstOrDefault();
            if (path == null)
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
    }
}