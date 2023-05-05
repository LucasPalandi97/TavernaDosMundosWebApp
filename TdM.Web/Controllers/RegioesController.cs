using Microsoft.AspNetCore.Mvc;
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
        var regiao = await regiaoRepository.GetByUrlHandleAsync(urlHandle, 1, 10);
        return View(regiao);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var regioes = await regiaoRepository.GetAllAsync(1, 10);
        return View(regioes);
    }
}