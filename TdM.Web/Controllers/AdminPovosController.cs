using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPovosController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IPersonagemRepository personagemRepository;
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IPovoRepository povoRepository;

    public AdminPovosController(IPovoRepository povoRepository, IMundoRepository mundoRepository
        , IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository
        , IPersonagemRepository personagemRepository, ICriaturaRepository criaturaRepository)
    {
        this.povoRepository = povoRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
        this.personagemRepository = personagemRepository;
        this.criaturaRepository = criaturaRepository;
    }

    public async Task<IActionResult> ListPovosByMundo(Guid id)
    {
        IEnumerable<Povo> povos;
        povos = await povoRepository.GetAllByMundoAsync(id);
        var selectListItems = povos.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos = await mundoRepository.GetAllAsync();
        var model = new AddPovoRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPovoRequest addPovoRequest)
    {

        //Map view model to domain model

        var povo = new Povo
        {
            Nome = addPovoRequest.Nome,
            CurtaDescricao = addPovoRequest.CurtaDescricao,
            Descricao = addPovoRequest.Descricao,
            ImgCard = addPovoRequest.ImgCard,
            ImgBox = addPovoRequest.ImgBox,
            PublishedDate = addPovoRequest.PublishedDate,
            UrlHandle = addPovoRequest.UrlHandle,
            Visible = addPovoRequest.Visible,

        };

        //Maps Mundos from Selected mundo
        var selectedMundoId = addPovoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Povos back to domain modal
                povo.Mundo = selectedMundo;
            }
        }
        //Maps Continents from Selected continent

        var selectedContinents = new List<Continente>();
        if (selectedContinents != null && selectedContinents.Any())
        {
            foreach (var selectedContinentId in addPovoRequest.SelectedContinentes)
            {
                var selectedContinentIdAsGuid = Guid.Parse(selectedContinentId);
                var existingContinent = await continenteRepository.GetAsync(selectedContinentIdAsGuid);

                if (existingContinent != null)
                {
                    selectedContinents.Add(existingContinent);
                }
            }
        }
        //Maping Continentes back to domain modal
        povo.Continentes = selectedContinents;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();
        if (selectedRegioes != null && selectedRegioes.Any())
        {
            foreach (var selectedRegiaoId in addPovoRequest.SelectedRegioes)
            {
                var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
                var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid);

                if (existingRegiao != null)
                {
                    selectedRegioes.Add(existingRegiao);
                }
            }
        }
        //Maping Regioes back to domain modal
        povo.Regioes = selectedRegioes;


        //Maps Personagens from Selected Regiao
        var selectedPersonagens = new List<Personagem>();
        if (selectedPersonagens != null && selectedPersonagens.Any())
        {
            foreach (var selectedPersonagemId in addPovoRequest.SelectedPersonagens)
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid);

                if (existingPersonagem != null)
                {
                    selectedPersonagens.Add(existingPersonagem);
                }
            }
        }
        //Maping Personagens back to domain modal
        povo.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Regiao
        var selectedCriaturas = new List<Criatura>();
        if (selectedCriaturas != null && selectedCriaturas.Any())
        {
            foreach (var selectedCriaturaId in addPovoRequest.SelectedCriaturas)
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid);

                if (existingCriatura != null)
                {
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
        //Maping Criaturas back to domain modal
        povo.Criaturas = selectedCriaturas;

        await povoRepository.AddAsync(povo);
        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the povo
        var povos = await povoRepository.GetAllAsync();
        return View(povos);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var povo = await povoRepository.GetAsync(id);
        var mundosDomainModel = await mundoRepository.GetAllAsync();
        var continentesDomainModel = await continenteRepository.GetAllAsync();
        var regioesDomainModel = await regiaoRepository.GetAllAsync();
        var personagensDomainModel = await personagemRepository.GetAllAsync();
        var criaturasDomainModel = await criaturaRepository.GetAllAsync();
        {
            if (povo != null)
            {   //Map the domain model into the view model
                var editPovoRequest = new EditPovoRequest
                {
                    Id = povo.Id,
                    Nome = povo.Nome,
                    CurtaDescricao = povo.CurtaDescricao,
                    Descricao = povo.Descricao,
                    ImgCard = povo.ImgCard,
                    ImgBox = povo.ImgBox,
                    PublishedDate = povo.PublishedDate,
                    UrlHandle = povo.UrlHandle,
                    Visible = povo.Visible,
                    Mundos = mundosDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedMundo = povo.Mundo?.Id.ToString(),
                    Continentes = continentesDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedContinentes = povo.Continentes.Select(x => x.Id.ToString()).ToArray(),
                    Regioes = regioesDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedRegioes = povo.Personagens.Select(x => x.Id.ToString()).ToArray(),
                    Personagens = personagensDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedPersonagens = povo.Personagens.Select(x => x.Id.ToString()).ToArray(),
                    Criaturas = criaturasDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedCriaturas = povo.Criaturas.Select(x => x.Id.ToString()).ToArray()
                };
                return View(editPovoRequest);
            }
            return View(null);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditPovoRequest editPovoRequest)
    {
        var povo = new Povo
        {
            Id = editPovoRequest.Id,
            Nome = editPovoRequest.Nome,
            CurtaDescricao = editPovoRequest.CurtaDescricao,
            Descricao = editPovoRequest.Descricao,
            ImgCard = editPovoRequest.ImgCard,
            ImgBox = editPovoRequest.ImgBox,
            PublishedDate = editPovoRequest.PublishedDate,
            UrlHandle = editPovoRequest.UrlHandle,
            Visible = editPovoRequest.Visible,


        };

        //Map Mundo into domain model
        var selectedMundoId = editPovoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Povos back to domain modal
                povo.Mundo = selectedMundo;
            }
        }

        var updatedPovo = await povoRepository.UpdateAsync(povo);

        if (updatedPovo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editPovoRequest.Id });
    }

    public async Task<IActionResult> Delete(EditPovoRequest editPovoRequest)
    {
        var deletedPovo = await povoRepository.DeleteAsync(editPovoRequest.Id);

        if (deletedPovo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editPovoRequest.Id });
    }


}