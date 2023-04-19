using Microsoft.AspNetCore.Mvc;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class PovosController : Controller
{
    private readonly IPovoRepository povoRepository;
    private readonly IMundoRepository mundoRepository;

    public PovosController(IPovoRepository povoRepository, IMundoRepository mundoRepository)
    {
        this.povoRepository = povoRepository;
        this.mundoRepository = mundoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var povo = await povoRepository.GetByUrlHandleAsync(urlHandle);
        return View(povo);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            var povos = await povoRepository.GetAllAsync();
            var viewModel = new NavbarViewModel
            {
                Povos = povos
            };
            return View(viewModel);
        }
        else
        {
            var mundo = await mundoRepository.GetByUrlHandleAsync(urlHandle);
            var povos = await povoRepository.GetAllByMundoAsync(mundo.Id);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel
            {
                Mundo = mundo,
                Povos = povos
            };
            return View(viewModel);
        }
    }
}

