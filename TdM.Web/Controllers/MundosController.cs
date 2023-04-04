using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TdM.Database.Data;
using TdM.Database.Models.Domain;
using TdM.Database.Models.Domain.Enums;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class MundosController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;

    public MundosController(IMundoRepository mundoRepository, IContinenteRepository continenteRepository)
    {
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
    }
    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> Index()
    {
        // Use dbContext to read the mundos
        var mundos = await mundoRepository.GetAllAsync();
        return View(mundos);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get continente from repository
        var continente = await continenteRepository.GetAllAsync();

        var model = new AddMundoRequest
        {
            Continentes = continente.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
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
            Autor = (Autor)addMundoRequest.Autor,
            ImgSrc = addMundoRequest.ImgSrc,
            Visible = addMundoRequest.Visible,
           
        };


        //Maps Continents from Selected continent

        var selectedContinents = new List<Continente>();
        foreach (var selectedContinentId in addMundoRequest.SelectedContinentes)
        {
            var selectedContinentIdAsGuid = Guid.Parse(selectedContinentId);
            var existingContinent = await continenteRepository.GetAsync(selectedContinentIdAsGuid);

            if (existingContinent != null)
            {
                selectedContinents.Add(existingContinent);
            }
        }
        //Maping Continentes back to domain modal
        mundo.Continentes = selectedContinents;

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
                Id = mundo.Id,
                Nome = mundo.Nome,
                Descricao = mundo.Descricao,
                Autor = mundo.Autor,
                ImgSrc = mundo.ImgSrc,
                Visible = mundo.Visible
                
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
            Id = editMundoRequest.Id,
            Nome = editMundoRequest.Nome,
            Descricao = editMundoRequest.Descricao,
            Autor = editMundoRequest.Autor,
            ImgSrc = editMundoRequest.ImgSrc,
            Visible = editMundoRequest.Visible
           
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

        return RedirectToAction("Edit", new { id = editMundoRequest.Id });
    }
    public async Task<IActionResult> Delete(EditMundoRequest editMundoRequest)
    {
        var deletedMundo = await mundoRepository.DeleteAsync(editMundoRequest.Id);

        if (deletedMundo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editMundoRequest.Id });
    }


}