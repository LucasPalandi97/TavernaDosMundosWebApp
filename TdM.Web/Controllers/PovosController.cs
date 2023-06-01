using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Web.Models;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class PovosController : Controller
{
    private readonly IPovoRepository povoRepository;
    private readonly IMundoRepository mundoRepository;

    public PovosController(IPovoRepository povoRepository, IMundoRepository mundoRepository)
    {
        this.povoRepository = povoRepository;
        this.mundoRepository = mundoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var povo = await povoRepository.GetByUrlHandleAsync(urlHandle, 1, 10);
        return View(povo);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            if (User.IsInRole("Admin"))
            {
                var povos = await povoRepository.GetAllAsync(1, 100);
                var viewModel = new NavbarViewModel
                {
                    Povos = povos
                };
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");

        }
        else
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
            var povos = await povoRepository.GetAllByMundoAsync(mundo.Id, 1, 100);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel
            {
                Mundo = mundo,
                Povos = povos
            };
            return View(viewModel);
        }
    }
}

