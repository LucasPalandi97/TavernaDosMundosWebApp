using Microsoft.AspNetCore.Mvc;
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
        var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle);
        return View(mundo);
    }
}
