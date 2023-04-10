using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class RegioesController : Controller
{
    private readonly IRegiaoRepository regiaoRepository;

    public RegioesController(IRegiaoRepository regiaoRepository)
    {
        this.regiaoRepository = regiaoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var regiao = await regiaoRepository.GetByUrlHandleAsync(urlHandle);
        return View(regiao);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var regioes = await regiaoRepository.GetAllAsync();
        return View(regioes);
    }
}