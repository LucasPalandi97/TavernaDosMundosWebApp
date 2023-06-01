using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Web.Models;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class ContosController : Controller
{
    private readonly IContoRepository contoRepository;
    private readonly IMundoRepository mundoRepository;

    public ContosController(IContoRepository contoRepository, IMundoRepository mundoRepository)
    {
        this.contoRepository = contoRepository;
        this.mundoRepository = mundoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var conto = await contoRepository.GetByUrlHandleAsync(urlHandle, 1, 10);
        return View(conto);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            if (User.IsInRole("Admin"))
            {
                var contos = await contoRepository.GetAllAsync(1, 100);
                var viewModel = new NavbarViewModel
                {
                    Contos = contos
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

            var contos = await contoRepository.GetAllByMundoAsync(mundo.Id, 1, 100);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel
            {
                Mundo = mundo,
                Contos = contos
            };
            return View(viewModel);
        }
    }
}

