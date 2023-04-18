using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using Microsoft.Build.Framework;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCriaturasController : Controller
{
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;

    public AdminCriaturasController(ICriaturaRepository criaturaRepository,IMundoRepository mundoRepository,IContinenteRepository continenteRepository , IRegiaoRepository regiaoRepository)
    {
        this.criaturaRepository = criaturaRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
    }

   
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos = await mundoRepository.GetAllAsync();
        var model = new AddCriaturaRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Add(AddCriaturaRequest addCriaturaRequest)
    {
       
        //Map view model to domain model

        var criatura = new Criatura
        {
            Nome = addCriaturaRequest.Nome,
            Tipo = addCriaturaRequest.Tipo,
            CurtaDescricao = addCriaturaRequest.CurtaDescricao,
            Descricao = addCriaturaRequest.Descricao,
            ImgCard = addCriaturaRequest.ImgCard,
            ImgBox = addCriaturaRequest.ImgBox,
            PublishedDate = addCriaturaRequest.PublishedDate,
            UrlHandle = addCriaturaRequest.UrlHandle,
            Visible = addCriaturaRequest.Visible,
           
        };

        //Maps Mundos from Selected mundo

        var selectedMundoId = addCriaturaRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                criatura.Mundo = selectedMundo;
            }
        }

        //Maps Continents from Selected continent

        var selectedContinents = new List<Continente>();
        foreach (var selectedContinentId in addCriaturaRequest.SelectedContinentes)
        {
            var selectedContinentIdAsGuid = Guid.Parse(selectedContinentId);
            var existingContinent = await continenteRepository.GetAsync(selectedContinentIdAsGuid);

            if (existingContinent != null)
            {
                selectedContinents.Add(existingContinent);
            }
        }
        //Maping Continentes back to domain modal
        criatura.Continentes = selectedContinents;

        //Maps Continents from Selected continent

        var selectedRegioes = new List<Regiao>();
        foreach (var selectedRegiaoId in addCriaturaRequest.SelectedRegioes)
        {
            var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
            var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid);

            if (existingRegiao != null)
            {
                selectedRegioes.Add(existingRegiao);
            }
        }

        //Maping Continentes back to domain modal
        criatura.Regioes = selectedRegioes;

        await criaturaRepository.AddAsync(criatura);

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the criatura
        var criaturas = await criaturaRepository.GetAllAsync();
        return View(criaturas);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var criatura = await criaturaRepository.GetAsync(id);
        var mundosDomainModel = await mundoRepository.GetAllAsync();
        var continentesDomainModel = await continenteRepository.GetAllAsync();
        var regioesDomainModel = await regiaoRepository.GetAllAsync();

        if (criatura != null)
        {   //Map the domain model into the view model
            var editCriaturaRequest = new EditCriaturaRequest
            {
                Id = criatura.Id,
                Nome = criatura.Nome,
                Tipo = criatura.Tipo,  
                CurtaDescricao = criatura.CurtaDescricao,  
                Descricao = criatura.Descricao,
                ImgCard = criatura.ImgCard,
                ImgBox = criatura.ImgBox,
                PublishedDate = criatura.PublishedDate,
                UrlHandle = criatura.UrlHandle,
                Visible = criatura.Visible,
                Mundos = mundosDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = criatura.Mundo?.Id.ToString(),
                Continentes = continentesDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinentes = criatura.Continentes.Select(x => x.Id.ToString()).ToArray(),
                Regioes = regioesDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegioes = criatura.Regioes.Select(x => x.Id.ToString()).ToArray()
            };
            
            return View(editCriaturaRequest);
    }
        return View(null);
}

    [HttpPost]
    public async Task<IActionResult> Edit(EditCriaturaRequest editCriaturaRequest)
    {
        var criatura = new Criatura
        {
            Id = editCriaturaRequest.Id,
            Nome = editCriaturaRequest.Nome,
            CurtaDescricao=editCriaturaRequest.CurtaDescricao,
            Descricao = editCriaturaRequest.Descricao,
            ImgCard = editCriaturaRequest.ImgCard,
            ImgBox = editCriaturaRequest.ImgBox,
            PublishedDate=editCriaturaRequest.PublishedDate,
            UrlHandle = editCriaturaRequest.UrlHandle,
            Visible = editCriaturaRequest.Visible,

        };

        //Map Mundo into domain model
        var selectedMundoId = editCriaturaRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                criatura.Mundo = selectedMundo;
            }
        }
        

        //Map Continentes into domain model
        var selectedContinentes = new List<Continente>();
        foreach (var selectedContinente in editCriaturaRequest.SelectedContinentes)
        {
            if (Guid.TryParse(selectedContinente, out var continente))
            {
                var foundContinente = await continenteRepository.GetAsync(continente);
                if (foundContinente != null)
                {
                    selectedContinentes.Add(foundContinente);
                }
            }

        }
        criatura.Continentes = selectedContinentes;

        //Map Regioes into domain model
        var selectedRegioes = new List<Regiao>();
        foreach (var selectedRegiao in editCriaturaRequest.SelectedRegioes)
        {
            if (Guid.TryParse(selectedRegiao, out var regiao))
            {
                var foundRegiao = await regiaoRepository.GetAsync(regiao);
                if (foundRegiao != null)
                {
                    selectedRegioes.Add(foundRegiao);
                }
            }

        }
        criatura.Regioes = selectedRegioes;

        //Submit information to repository
        var updatedCriatura = await criaturaRepository.UpdateAsync(criatura);

        if (updatedCriatura != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editCriaturaRequest.Id });
    }

    public async Task<IActionResult> Delete(EditCriaturaRequest editCriaturaRequest)
    {
        var deletedCriatura = await criaturaRepository.DeleteAsync(editCriaturaRequest.Id);

        if (deletedCriatura != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editCriaturaRequest.Id });
    }

         
}