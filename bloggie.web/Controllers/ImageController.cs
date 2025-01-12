using System.Net;
using Bloggie.Web.Repositries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            var image = await imageRepository.UploadAsync(formFile);
            if (image == null)
            {
                return Problem("something went wrong", null, (int)HttpStatusCode.BadRequest);
            }
            else { 
                return new JsonResult(new { link = image });
            }
        }
    }
}
