using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Web.Models;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class CriaturasController : Controller
{
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IMundoRepository mundoRepository;

    public CriaturasController(ICriaturaRepository criaturaRepository, IMundoRepository mundoRepository)
    {
        this.criaturaRepository = criaturaRepository;
        this.mundoRepository = mundoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var criatura = await criaturaRepository.GetByUrlHandleAsync(urlHandle, 1, 10);
        return View(criatura);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            if (User.IsInRole("Admin"))
            {
                var criaturas = await criaturaRepository.GetAllAsync(1, 10);
                var viewModel = new NavbarViewModel
                {
                    Criaturas = criaturas
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

            var criaturas = await criaturaRepository.GetAllByMundoAsync(mundo.Id, 1, 10);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel
            {
                Mundo = mundo,
                Criaturas = criaturas
            };
            return View(viewModel);
        }
    }
}

