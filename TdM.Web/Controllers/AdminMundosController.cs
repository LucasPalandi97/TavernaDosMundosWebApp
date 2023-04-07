using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using TdM.Database.Models.Domain.Enums;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

public class AdminMundosController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;

    public AdminMundosController(IMundoRepository mundoRepository, IContinenteRepository continenteRepository)
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
            CurtaDescricao = addMundoRequest.CurtaDescricao,
            Autor = (Autor)addMundoRequest.Autor,
            ImgBox = addMundoRequest.ImgBox,
            PublishedDate = addMundoRequest.PublishedDate,
            UrlHandle = addMundoRequest.UrlHandle,
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
        //Retrieve result from repositoty
        var mundo = await mundoRepository.GetAsync(id);
        var contientesDomainModel = await continenteRepository.GetAllAsync();

        if (mundo != null)
        {
            //Map the domain model into the view model
            var editMundoRequest = new EditMundoRequest
            {
                Id = mundo.Id,
                Nome = mundo.Nome,
                CurtaDescricao = mundo.CurtaDescricao,  
                Descricao = mundo.Descricao,
                Autor = mundo.Autor,               
                ImgBox = mundo.ImgBox,
                PublishedDate = mundo.PublishedDate,
                UrlHandle   = mundo.UrlHandle,
                Visible = mundo.Visible,
                Continentes = contientesDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinentes = mundo.Continentes.Select(x => x.Id.ToString()).ToArray(),


            };
            return View(editMundoRequest);
        }
        return View(null);
    }
    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> Edit(EditMundoRequest editMundoRequest)
    {
        //Map view model back to damain model
        var mundo = new Mundo
        {
            Id = editMundoRequest.Id,
            Nome = editMundoRequest.Nome,
            CurtaDescricao = editMundoRequest.CurtaDescricao,
            Descricao = editMundoRequest.Descricao,
            Autor = editMundoRequest.Autor,
            ImgBox = editMundoRequest.ImgBox,
            PublishedDate = editMundoRequest.PublishedDate,
            UrlHandle = editMundoRequest.UrlHandle, 
            Visible = editMundoRequest.Visible,
        };

        //Map Continentes into domain model
        var selectedContinentes = new List<Continente>();
        foreach (var selectedContinent in editMundoRequest.SelectedContinentes)
        {
            if (Guid.TryParse(selectedContinent, out var tag))
            {
                var foundContinente = await continenteRepository.GetAsync(tag);
                if (foundContinente != null)
                {
                    selectedContinentes.Add(foundContinente);
                }
            }

        }

        mundo.Continentes = selectedContinentes;

        //Submit information to repository
        var updatedMundo = await mundoRepository.UpdateAsync(mundo);

        if (updatedMundo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification
            return RedirectToAction("Edit", new { id = editMundoRequest.Id });
        }
        //Redirect to Get
    }
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(EditMundoRequest editMundoRequest)
    {
        // Talk to repository to delete this mundo and tags
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