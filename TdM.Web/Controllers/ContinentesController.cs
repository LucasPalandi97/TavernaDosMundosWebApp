using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Database.Models.Domain;
using TdM.Web.Models;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class ContinentesController : Controller
{
    private readonly IContinenteRepository continenteRepository;
    private readonly IMundoRepository mundoRepository;
    private readonly IRegiaoRepository regiaoRepository;

    public ContinentesController(IContinenteRepository continenteRepository, IMundoRepository mundoRepository, IRegiaoRepository regiaoRepository)
    {
        this.continenteRepository = continenteRepository;
        this.mundoRepository = mundoRepository;
        this.regiaoRepository = regiaoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var continente = await continenteRepository.GetByUrlHandleAsync(urlHandle);
        return View(continente);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            if (User.IsInRole("Admin"))
            {
                var continentes = await continenteRepository.GetAllAsync();
                var regioes = await regiaoRepository.GetAllAsync();
                var viewModel = new NavbarViewModel
                {
                    Continentes = continentes,
                    Regioes = regioes
                };
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");

        }
        else
        {
            var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle);

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

            var continentes = await continenteRepository.GetAllByMundoAsync(mundo.Id);
            var regioes = await regiaoRepository.GetAllByMundoAsync(mundo.Id);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel

            {
                Mundo = mundo,
                Continentes = continentes,
                Regioes = regioes
            };

            return View(viewModel);
        }
    }
}



