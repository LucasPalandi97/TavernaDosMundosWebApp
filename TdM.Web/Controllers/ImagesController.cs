using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageRepository imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
        this.imageRepository = imageRepository;
    }
    [HttpPost]
    public async Task<IActionResult> UploadAsync(IFormFile file)
    {
        // Call a repository
        var imageURL = await imageRepository.UploadAsync(file);
         
        if(imageURL == null)
        {
            return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
        }

        return new JsonResult(new { link= imageURL });

    }
 
}