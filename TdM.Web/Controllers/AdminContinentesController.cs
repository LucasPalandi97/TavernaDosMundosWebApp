using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminContinentesController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;

    public AdminContinentesController(IMundoRepository mundoRepository, IContinenteRepository continenteRepository)
    {
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
    }
    [HttpGet]

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos  = await mundoRepository.GetAllAsync();
        var model = new AddContinenteRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model); 
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddContinenteRequest addContinenteRequest)
    {

        //Map view model to domain model

        var continente = new Continente
        {
            Nome = addContinenteRequest.Nome,
            CurtaDescricao = addContinenteRequest.CurtaDescricao,
            Descricao = addContinenteRequest.Descricao,
            ImgCard = addContinenteRequest.ImgCard,
            ImgBox = addContinenteRequest.ImgBox,
            PublishedDate = addContinenteRequest.PublishedDate,
            UrlHandle = addContinenteRequest.UrlHandle,
            Visible = addContinenteRequest.Visible,
            

        };

        //Maps Mundos from Selected mundo
     
        var selectedMundoId = addContinenteRequest.SelectedMundo;
        if (selectedMundoId !=null) { 
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
            var selectedMundo = existingMundo;
            //Maping Continentes back to domain modal
            continente.Mundo = selectedMundo;
            }
        }
        await continenteRepository.AddAsync(continente);

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the continente
        var continentes = await continenteRepository.GetAllAsync();
        return View(continentes);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var continente = await continenteRepository.GetAsync(id);
        var mundosDomainModel = await mundoRepository.GetAllAsync();

        if (continente != null)
        {
            var editContinenteRequest = new EditContinenteRequest
            {
                Id = continente.Id,
                Nome = continente.Nome,
                CurtaDescricao = continente.CurtaDescricao,  
                Descricao = continente.Descricao,
                ImgCard = continente.ImgCard,
                ImgBox = continente.ImgBox,
                PublishedDate = continente.PublishedDate,
                UrlHandle = continente.UrlHandle,
                Visible = continente.Visible,
                Mundos = mundosDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = continente.Mundo?.Id.ToString()
               
            }; 
        return View(editContinenteRequest);
    }
        return View(null);
}

    [HttpPost]
    public async Task<IActionResult> Edit(EditContinenteRequest editContinenteRequest)
    {
        var continente = new Continente
        {
            Id = editContinenteRequest.Id,
            Nome = editContinenteRequest.Nome,
            CurtaDescricao=editContinenteRequest.CurtaDescricao,
            Descricao = editContinenteRequest.Descricao,
            ImgCard = editContinenteRequest.ImgCard,
            ImgBox = editContinenteRequest.ImgBox,
            PublishedDate=editContinenteRequest.PublishedDate,
            UrlHandle = editContinenteRequest.UrlHandle,
            Visible = editContinenteRequest.Visible,

        };

        //Map Mundo into domain model
        var selectedMundoId = editContinenteRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                continente.Mundo = selectedMundo;
            }
        }
        var updatedContinente = await continenteRepository.UpdateAsync(continente);

        if (updatedContinente != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editContinenteRequest.Id });
    }

    public async Task<IActionResult> Delete(EditContinenteRequest editContinenteRequest)
    {
        var deletedContinente = await continenteRepository.DeleteAsync(editContinenteRequest.Id);

        if (deletedContinente != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editContinenteRequest.Id });
    }

         
}