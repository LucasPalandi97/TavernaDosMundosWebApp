using Microsoft.AspNetCore.Mvc;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class CriaturasController : Controller
{
    private readonly ICriaturaRepository criaturaRepository;

    public CriaturasController(ICriaturaRepository criaturaRepository)
    {
        this.criaturaRepository = criaturaRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var criatura = await criaturaRepository.GetByUrlHandleAsync(urlHandle);
        return View(criatura);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var criatura = await criaturaRepository.GetAllAsync();
        return View(criatura);
    }
}
