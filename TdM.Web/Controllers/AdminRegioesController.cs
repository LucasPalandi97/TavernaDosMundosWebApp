using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminRegioesController : Controller
{
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IContinenteRepository continenteRepository;

    public AdminRegioesController(IRegiaoRepository regiaoRepository, IContinenteRepository continenteRepository)
    {
        this.regiaoRepository = regiaoRepository;
        this.continenteRepository = continenteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get continente from repository
        var continente = await continenteRepository.GetAllAsync();
        var model = new AddRegiaoRequest
        {
            Continentes = continente.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddRegiaoRequest addRegiaoRequest)
    {

        //Map view model to domain model

        var regiao = new Regiao
        {
            Nome = addRegiaoRequest.Nome,
            CurtaDescricao = addRegiaoRequest.CurtaDescricao,
            Descricao = addRegiaoRequest.Descricao,
            Simbolo = addRegiaoRequest.Simbolo,
            ImgCard = addRegiaoRequest.ImgCard,
            ImgBox = addRegiaoRequest.ImgBox,
            PublishedDate = addRegiaoRequest.PublishedDate,
            UrlHandle = addRegiaoRequest.UrlHandle,
            Visible = addRegiaoRequest.Visible,


        };

        //Maps Continentes from Selected Continente

        var selectedContinenteId = addRegiaoRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Continentes back to domain modal
                regiao.Continente = selectedContinente;
                regiao.Mundo = selectedContinente.Mundo;
            }
        }
        await regiaoRepository.AddAsync(regiao);

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the continente
        var continentes = await regiaoRepository.GetAllAsync();
        return View(continentes);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var regiao = await regiaoRepository.GetAsync(id);
        var continenteDomainModel = await continenteRepository.GetAllAsync();

        if (regiao != null)
        {
            var editRegiaoRequest = new EditRegiaoRequest
            {
                Id = regiao.Id,
                Nome = regiao.Nome,
                CurtaDescricao = regiao.CurtaDescricao,
                Descricao = regiao.Descricao,
                Simbolo = regiao.Simbolo,
                ImgCard = regiao.ImgCard,
                ImgBox = regiao.ImgBox,
                PublishedDate = regiao.PublishedDate,
                UrlHandle = regiao.UrlHandle,
                Visible = regiao.Visible,
                Continentes = continenteDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinente = regiao.Continente?.Id.ToString()
              
        }; 
       
        return View(editRegiaoRequest);
        }
        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditRegiaoRequest editRegiaoRequest)
    {
        var regiao = new Regiao
        {
            Id = editRegiaoRequest.Id,
            Nome = editRegiaoRequest.Nome,
            CurtaDescricao = editRegiaoRequest.CurtaDescricao,
            Descricao = editRegiaoRequest.Descricao,
            Simbolo = editRegiaoRequest.Simbolo,
            ImgCard = editRegiaoRequest.ImgCard,
            ImgBox = editRegiaoRequest.ImgBox,
            PublishedDate = editRegiaoRequest.PublishedDate,
            UrlHandle = editRegiaoRequest.UrlHandle,
            Visible = editRegiaoRequest.Visible,

        };

        //Map Continente into domain model
        var selectedContinenteId = editRegiaoRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Continentes back to domain modal
                regiao.Continente = selectedContinente;
                regiao.Mundo = selectedContinente.Mundo;
            }

        }

        var updatedRegiao = await regiaoRepository.UpdateAsync(regiao);

        if (updatedRegiao != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editRegiaoRequest.Id });
    }

    public async Task<IActionResult> Delete(EditRegiaoRequest editRegiaoRequest)
    {
        var deletedRegiao = await regiaoRepository.DeleteAsync(editRegiaoRequest.Id);

        if (deletedRegiao != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editRegiaoRequest.Id });
    }
}