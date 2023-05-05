using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Web.Models;
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
        var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle, 1, 10);

        if (mundo == null)
        {
            // return a custom error view with an appropriate message             
            var errorViewModel = new ErrorViewModel
            {
                ErrorMessage = "The specified URL handle does not exist.",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View("Error", errorViewModel);
        }

        var navbarViewModel = new NavbarViewModel
        {
            Mundo = mundo,
            MundoUrlHandle = mundo.UrlHandle,
        };

        return View(navbarViewModel);
    }
}
