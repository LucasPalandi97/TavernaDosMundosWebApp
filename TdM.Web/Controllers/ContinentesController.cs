using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class ContinentesController : Controller
{
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;

    public ContinentesController(IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository)
    {
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var continente = await continenteRepository.GetByUrlHandleAsync(urlHandle);
        return View(continente);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        //get all continentes
        var continentes = await continenteRepository.GetAllAsync();

        //get all regions
        var regioes = await regiaoRepository.GetAllAsync();

        var model = new RegionViewModel
        {
            Continentes = continentes,
            Regioes = regioes
        };
        return View(model);
    }
}


