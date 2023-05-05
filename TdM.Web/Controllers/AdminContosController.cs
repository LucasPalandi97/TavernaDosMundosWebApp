﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminContosController : Controller
{
    private readonly IContoRepository contoRepository;
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IPersonagemRepository personagemRepository;
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IPovoRepository povoRepository;

    public AdminContosController(IContoRepository contoRepository, IMundoRepository mundoRepository
        , IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository
        , IPersonagemRepository personagemRepository, ICriaturaRepository criaturaRepository, IPovoRepository povoRepository)
    {
        this.contoRepository = contoRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
        this.personagemRepository = personagemRepository;
        this.criaturaRepository = criaturaRepository;
        this.povoRepository = povoRepository;
    }

    public async Task<IActionResult> ListContosByMundo(Guid id)
    {
        IEnumerable<Conto> contos = await contoRepository.GetAllByMundoAsync(id);
        var selectListItems = contos.Select(x => new SelectListItem
        {
            Text = x.Titulo,
            Value = x.Id.ToString()
        });
        return Json(selectListItems);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos = await mundoRepository.GetAllAsync();
        var model = new AddContoRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddContoRequest addContoRequest)
    {

        if (!ModelState.IsValid)
        {
            addContoRequest.Mundos = (await mundoRepository.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addContoRequest.Continentes = (await continenteRepository.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addContoRequest.Regioes = (await regiaoRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addContoRequest.Personagens = (await personagemRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addContoRequest.Criaturas = (await criaturaRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addContoRequest.Povos = (await povoRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            return View(addContoRequest);
        }

        //Map view model to domain model
        var conto = new Conto
        {
            Titulo = addContoRequest.Titulo,
            Corpo = addContoRequest.Corpo,
            Autor = addContoRequest.Autor,
            AudioDrama = addContoRequest.AudioDrama,
            ImgCard = addContoRequest.ImgCard,
            ImgBox = addContoRequest.ImgBox,
            PublishedDate = addContoRequest.PublishedDate,
            UrlHandle = addContoRequest.UrlHandle,
            Visible = addContoRequest.Visible,
        };

        //Maps Mundos from Selected mundo
        var selectedMundoId = addContoRequest.SelectedMundo;
        if (!string.IsNullOrEmpty(selectedMundoId))
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                conto.Mundo = selectedMundo;
            }
        }

        //Maps Continents from Selected continent
        var selectedContinentes = new List<Continente>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedContinenteId in addContoRequest.SelectedContinentes)
        {
            if (!string.IsNullOrEmpty(selectedContinenteId))
            {
                var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
                var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

                if (existingContinente != null)
                {
                    selectedContinentes.Add(existingContinente);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Continentes back to domain modal
        conto.Continentes = selectedContinentes;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedRegiaoId in addContoRequest.SelectedRegioes)
        {
            if (!string.IsNullOrEmpty(selectedRegiaoId))
            {
                var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
                var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid);

                if (existingRegiao != null)
                {
                    selectedRegioes.Add(existingRegiao);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Regioes back to domain modal
        conto.Regioes = selectedRegioes;

        //Maps Personagens from Selected Regiao
        var selectedPersonagens = new List<Personagem>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPersonagemId in addContoRequest.SelectedPersonagens)
        {
            if (!string.IsNullOrEmpty(selectedPersonagemId))
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid);

                if (existingPersonagem != null)
                {
                    selectedPersonagens.Add(existingPersonagem);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Personagens back to domain modal
        conto.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Regiao
        var selectedCriaturas = new List<Criatura>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedCriaturaId in addContoRequest.SelectedCriaturas)
        {
            if (!string.IsNullOrEmpty(selectedCriaturaId))
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid);

                if (existingCriatura != null)
                {
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Criaturas back to domain modal
        conto.Criaturas = selectedCriaturas;


        //Maps Povos from Selected Regiao
        var selectedPovos = new List<Povo>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPovoId in addContoRequest.SelectedPovos)
        {
            if (!string.IsNullOrEmpty(selectedPovoId))
            {
                var selectedPovoIdAsGuid = Guid.Parse(selectedPovoId);
                var existingPovo = await povoRepository.GetAsync(selectedPovoIdAsGuid);

                if (existingPovo != null)
                {
                    selectedPovos.Add(existingPovo);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Povos back to domain modal
        conto.Povos = selectedPovos;

        await contoRepository.AddAsync(conto);
        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the conto
        var contos = await contoRepository.GetAllAsync();
        return View(contos);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var conto = await contoRepository.GetAsync(id);
        var mundosDomainModel = await mundoRepository.GetAllAsync();
        var continentesDomainModel = await continenteRepository.GetAllAsync();
        var regioesDomainModel = await regiaoRepository.GetAllAsync();
        var personagensDomainModel = await personagemRepository.GetAllAsync();
        var criaturasDomainModel = await criaturaRepository.GetAllAsync();
        var povosDomainModel = await povoRepository.GetAllAsync();
        {
            if (conto != null)
            {   //Map the domain model into the view model
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                var editContoRequest = new EditContoRequest
                {
                    Id = conto.Id,
                    Titulo = conto.Titulo,
                    Corpo = conto.Corpo,
                    Autor = conto.Autor,
                    AudioDrama = conto.AudioDrama,
                    ImgCard = conto.ImgCard,
                    ImgBox = conto.ImgBox,
                    PublishedDate = conto.PublishedDate,
                    UrlHandle = conto.UrlHandle,
                    Visible = conto.Visible,
                    Mundos = mundosDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedMundo = conto.Mundo?.Id.ToString(),
                    Continentes = continentesDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedContinentes = conto.Continentes.Select(x => x.Id.ToString()).ToArray(),
                    Regioes = regioesDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedRegioes = conto.Regioes.Select(x => x.Id.ToString()).ToArray(),
                    Personagens = personagensDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedPersonagens = conto.Personagens.Select(x => x.Id.ToString()).ToArray(),
                    Criaturas = criaturasDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedCriaturas = conto.Criaturas.Select(x => x.Id.ToString()).ToArray(),
                    Povos = povosDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedPovos = conto.Povos.Select(x => x.Id.ToString()).ToArray()
                };
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
                return View(editContoRequest);
            }
            return View(null);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditContoRequest editContoRequest)
    {
        if (!ModelState.IsValid)
        {
            editContoRequest.Mundos = (await mundoRepository.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            editContoRequest.Continentes = (await continenteRepository.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            editContoRequest.Regioes = (await regiaoRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            editContoRequest.Personagens = (await personagemRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            editContoRequest.Criaturas = (await criaturaRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            editContoRequest.Povos = (await povoRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            return View(editContoRequest);
        }

        var conto = new Conto
        {
            Id = editContoRequest.Id,
            Titulo = editContoRequest.Titulo,
            Corpo = editContoRequest.Corpo,
            Autor = editContoRequest.Autor,
            AudioDrama = editContoRequest.AudioDrama,
            ImgCard = editContoRequest.ImgCard,
            ImgBox = editContoRequest.ImgBox,
            PublishedDate = editContoRequest.PublishedDate,
            UrlHandle = editContoRequest.UrlHandle,
            Visible = editContoRequest.Visible,
        };

        //Maps Mundos from Selected mundo
        var selectedMundoId = editContoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                conto.Mundo = selectedMundo;
            }
        }
        //Maps Continents from Selected continent
        var selectedContinentes = new List<Continente>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedContinenteId in editContoRequest.SelectedContinentes)
        {
            if (!string.IsNullOrEmpty(selectedContinenteId))
            {
                var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
                var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

                if (existingContinente != null)
                {
                    selectedContinentes.Add(existingContinente);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Continentes back to domain modal
        conto.Continentes = selectedContinentes;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedRegiaoId in editContoRequest.SelectedRegioes)
        {
            if (!string.IsNullOrEmpty(selectedRegiaoId))
            {
                var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
                var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid);

                if (existingRegiao != null)
                {
                    selectedRegioes.Add(existingRegiao);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Regioes back to domain modal
        conto.Regioes = selectedRegioes;

        //Maps Personagens from Selected Regiao
        var selectedPersonagens = new List<Personagem>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPersonagemId in editContoRequest.SelectedPersonagens)
        {
            if (!string.IsNullOrEmpty(selectedPersonagemId))
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid);

                if (existingPersonagem != null)
                {
                    selectedPersonagens.Add(existingPersonagem);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Personagens back to domain modal
        conto.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Regiao
        var selectedCriaturas = new List<Criatura>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedCriaturaId in editContoRequest.SelectedCriaturas)
        {
            if (!string.IsNullOrEmpty(selectedCriaturaId))
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid);

                if (existingCriatura != null)
                {
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Criaturas back to domain modal
        conto.Criaturas = selectedCriaturas;

        //Maps Povos from Selected Regiao
        var selectedPovos = new List<Povo>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPovoId in editContoRequest.SelectedPovos)
        {
            if (!string.IsNullOrEmpty(selectedPovoId))
            {
                var selectedPovoIdAsGuid = Guid.Parse(selectedPovoId);
                var existingPovo = await povoRepository.GetAsync(selectedPovoIdAsGuid);

                if (existingPovo != null)
                {
                    selectedPovos.Add(existingPovo);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Povos back to domain modal
        conto.Povos = selectedPovos;

        var updatedConto = await contoRepository.UpdateAsync(conto);

        if (updatedConto != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editContoRequest.Id });
    }

    public async Task<IActionResult> Delete(EditContoRequest editContoRequest)
    {
        var deletedConto = await contoRepository.DeleteAsync(editContoRequest.Id);

        if (deletedConto != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editContoRequest.Id });
    }
}