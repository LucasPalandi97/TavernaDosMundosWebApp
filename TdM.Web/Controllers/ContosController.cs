using Microsoft.AspNetCore.Mvc;
using TdM.Database.Models.Domain;
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
        var conto = await contoRepository.GetByUrlHandleAsync(urlHandle);
        return View(conto);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            var contos = await contoRepository.GetAllAsync();
            var viewModel = new NavbarViewModel
            {
                Contos = contos
            };
            return View(viewModel);
        }
        else
        {
            var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle);
            var contos = await contoRepository.GetAllByMundoAsync(mundo.Id);
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

