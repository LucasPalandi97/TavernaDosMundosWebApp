using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TdM.Web.Data;
using TdM.Web.Models.Domain;
using TdM.Web.Models.Domain.Enums;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositores;

namespace TdM.Web.Controllers;

public class AdminMundosController : Controller
{
    private readonly IMundoRepository mundoRepository;

    public AdminMundosController(IMundoRepository mundoRepository)
    {
        this.mundoRepository = mundoRepository;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> Add(AddMundoRequest addMundoRequest)
    {
        //Maping AddMundoRequest to Mundo domain model
        var mundo = new Mundo
        {
            Nome = addMundoRequest.Nome,
            Descricao = addMundoRequest.Descricao,
            Autor = (Autor)addMundoRequest.Autor
        };
        await mundoRepository.AddAsync(mundo);

        return RedirectToAction("List");
    }

    [HttpGet]
    [ActionName("List")]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the mundos
        var mundos = await mundoRepository.GetAllAsync();
        return View(mundos);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
       var mundo = await mundoRepository.GetAsync(id);

        if (mundo != null)
        {
            var editMundoRequest = new EditMundoRequest
            {
                MundoId = mundo.MundoId,
                Nome = mundo.Nome,
                Descricao = mundo.Descricao,
                Autor = mundo.Autor
            };
            return View(editMundoRequest);
        }
        return View(null);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditMundoRequest editMundoRequest)
    {
        var mundo = new Mundo
        {
            MundoId = editMundoRequest.MundoId,
            Nome = editMundoRequest.Nome,
            Descricao = editMundoRequest.Descricao,
            Autor = editMundoRequest.Autor
        };

        var updatedMundo = await mundoRepository.UpdateAsync(mundo);

        if (updatedMundo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editMundoRequest.MundoId });
    }
    public async Task<IActionResult> Delete(EditMundoRequest editMundoRequest)
    {
        var deletedMundo = await mundoRepository.DeleteAsync(editMundoRequest.MundoId);

        if (deletedMundo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editMundoRequest.MundoId });
    }
}