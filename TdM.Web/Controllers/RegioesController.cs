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
        var regiao = await regiaoRepository.GetByUrlHandleAsync(urlHandle);
        return View(regiao);
    }
}