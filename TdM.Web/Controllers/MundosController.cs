using Microsoft.AspNetCore.Mvc;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class MundosController : Controller
{
    private readonly IMundoRepository mundoRepository;

    public MundosController(IMundoRepository mundoRepository)
    {
        this.mundoRepository = mundoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {    
        var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle);

        var navbarViewModel = new NavbarViewModel 
        {
            Mundo = mundo,
            MundoUrlHandle = mundo.UrlHandle,       
        };

        return View(navbarViewModel);
    }
}
