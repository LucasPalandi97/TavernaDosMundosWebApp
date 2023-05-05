using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TdM.Web.Models;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;
namespace TdM.Web.Controllers;


public class PersonagensController : Controller
{
    private readonly IPersonagemRepository personagemRepository;
    private readonly IMundoRepository mundoRepository;

    public PersonagensController(IPersonagemRepository personagemRepository, IMundoRepository mundoRepository)
    {
        this.personagemRepository = personagemRepository;
        this.mundoRepository = mundoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var personagem = await personagemRepository.GetByUrlHandleAsync(urlHandle);
        return View(personagem);
    }

    [HttpGet]
    public async Task<IActionResult> List(string urlHandle)
    {
        if (urlHandle == null)
        {
            if (User.IsInRole("Admin"))
            {
                var personagens = await personagemRepository.GetAllAsync();
                var viewModel = new NavbarViewModel
                {
                    Personagens = personagens
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

            var personagens = await personagemRepository.GetAllByMundoAsync(mundo.Id);
            ViewBag.MundoUrlHandle = mundo.UrlHandle; // set the value of ViewBag here
            var viewModel = new NavbarViewModel
            {
                Mundo = mundo,
                Personagens = personagens
            };
            return View(viewModel);
        }
    }
}
