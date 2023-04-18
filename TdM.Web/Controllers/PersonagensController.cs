using Microsoft.AspNetCore.Mvc;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class PersonagensController : Controller
{
    private readonly IPersonagemRepository personagemRepository;

    public PersonagensController(IPersonagemRepository personagemRepository)
    {
        this.personagemRepository = personagemRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var personagem = await personagemRepository.GetByUrlHandleAsync(urlHandle);
        return View(personagem);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var personagens = await personagemRepository.GetAllAsync();
        return View(personagens);
    }
}