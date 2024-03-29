﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Drawing;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPovosController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IPersonagemRepository personagemRepository;
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IContoRepository contoRepository;
    private readonly IPovoRepository povoRepository;

    public AdminPovosController(IPovoRepository povoRepository, IMundoRepository mundoRepository
        , IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository
        , IPersonagemRepository personagemRepository, ICriaturaRepository criaturaRepository, IContoRepository contoRepository)
    {
        this.povoRepository = povoRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
        this.personagemRepository = personagemRepository;
        this.criaturaRepository = criaturaRepository;
        this.contoRepository = contoRepository;
    }

    public async Task<IActionResult> ListPovosByMundo(Guid id)
    {
        IEnumerable<Povo> povos;
        povos = await povoRepository.GetAllByMundoAsync(id, 1, 100);
        var orderedPovos = povos.OrderBy(x => x.Nome);
        var selectListItems = orderedPovos.Select(x => new SelectListItem
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
        var mundos = await mundoRepository.GetAllAsync(1, 100);
        var model = new AddPovoRequest
        {
            Mundos = mundos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddPovoRequest addPovoRequest)
    {
        await ValidateAddPovoRequest(addPovoRequest);

        if (!ModelState.IsValid)
        {
            addPovoRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addPovoRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addPovoRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addPovoRequest.Personagens = (await personagemRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addPovoRequest.Criaturas = (await criaturaRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addPovoRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
               .Select(x => new SelectListItem
               {
                   Text = x.Titulo,
                   Value = x.Id.ToString()
               }).ToList();

            return View(addPovoRequest);
        }


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
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                povo.Mundo = selectedMundo;
            }
        }
        //Maps Continents from Selected continent
        var selectedContinentes = new List<Continente>();
        foreach (var selectedContinenteId in addPovoRequest.SelectedContinentes)
        {
            if (!string.IsNullOrEmpty(selectedContinenteId))
            {
                var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
                var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid, 1, 10);

                if (existingContinente != null)
                {
                    selectedContinentes.Add(existingContinente);
                }
            }
        }
        //Maping Continentes back to domain modal
        povo.Continentes = selectedContinentes;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();

        foreach (var selectedRegiaoId in addPovoRequest.SelectedRegioes)
        {
            if (!string.IsNullOrEmpty(selectedRegiaoId))
            {
                var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
                var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid, 1, 10);

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

        foreach (var selectedPersonagemId in addPovoRequest.SelectedPersonagens)
        {
            if (!string.IsNullOrEmpty(selectedPersonagemId))
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid, 1, 10);

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

        foreach (var selectedCriaturaId in addPovoRequest.SelectedCriaturas)
        {
            if (!string.IsNullOrEmpty(selectedCriaturaId))
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid, 1, 10);

                if (existingCriatura != null)
                {
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
        //Maping Criaturas back to domain modal
        povo.Criaturas = selectedCriaturas;

        await povoRepository.AddAsync(povo);

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in addPovoRequest.SelectedContos)
        {
            if (!string.IsNullOrEmpty(selectedContoId))
            {
                var selectedContoIdAsGuid = Guid.Parse(selectedContoId);
                var existingConto = await contoRepository.GetAsync(selectedContoIdAsGuid, 1, 10);

                if (existingConto != null)
                {
                    selectedContos.Add(existingConto);
                }
            }
        }
        //Maping Contos back to domain modal
        povo.Contos = selectedContos;

        return RedirectToAction("List");
    }

    public async Task<IActionResult> ListPovosByRegiao(Guid id, List<Guid> selectedRegiaoIds = null)
    {
        IEnumerable<Povo> povos;
        if (selectedRegiaoIds == null)
        {
            povos = await povoRepository.GetAllByRegiaoAsync(id, 1, 100);
        }
        else
        {
            povos = await povoRepository.GetAllByRegiaoAsync(selectedRegiaoIds, 1, 100);
        }
        var orderedPovos = povos.OrderBy(x => x.Nome);
        var selectListItems = orderedPovos.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the povo
        var povos = await povoRepository.GetAllAsync(1, 100);
        return View(povos);
    }

    [HttpGet]
    public async Task<IActionResult> PeopleBuildModal(Guid povoId)
    {
        var povo = await povoRepository.GetAsync(povoId, 1, 10);
        return PartialView("_PeopleBuildModal", povo);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var povo = await povoRepository.GetAsync(id, 1, 10);
        var mundosDomainModel = await mundoRepository.GetAllAsync(1, 100);
        var continentesDomainModel = await continenteRepository.GetAllAsync(1, 100);
        var regioesDomainModel = await regiaoRepository.GetAllAsync(1, 100);
        var personagensDomainModel = await personagemRepository.GetAllAsync(1, 100);
        var criaturasDomainModel = await criaturaRepository.GetAllAsync(1, 010);
        var contosDomainModel = await contoRepository.GetAllAsync(1, 100);
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

                    Mundos = mundosDomainModel.OrderBy(x => x.Nome)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedMundo = povo.Mundo?.Id.ToString(),
                    Continentes = continentesDomainModel.Where(x => x.Mundo == povo.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedContinentes = povo.Continentes?.Select(x => x.Id.ToString()).ToArray(),
                    Regioes = regioesDomainModel.Where(x => povo.Continentes.SelectMany(c => c.Regioes).Contains(x)).OrderBy(x => x.Nome)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedRegioes = povo.Regioes?.Select(x => x.Id.ToString()).ToArray(),
                    Personagens = personagensDomainModel.Where(x => x.Mundo == povo.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedPersonagens = povo.Personagens?.Select(x => x.Id.ToString()).ToArray(),
                    Criaturas = criaturasDomainModel.Where(x => x.Mundo == povo.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Nome,
                        Value = x.Id.ToString()
                    }),
                    SelectedCriaturas = povo.Criaturas?.Select(x => x.Id.ToString()).ToArray(),
                    Contos = contosDomainModel.Where(x => x.Mundo == povo.Mundo && x.Mundo != null).OrderBy(x => x.Titulo)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Titulo,
                        Value = x.Id.ToString()
                    }),
                    SelectedContos = povo.Contos?.Select(x => x.Id.ToString()).ToArray()
                };
                return View(editPovoRequest);
            }
            return View(null);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditPovoRequest editPovoRequest)
    {
        await ValidateEditPovoRequest(editPovoRequest);

        if (!ModelState.IsValid)
        {
            editPovoRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
          .Select(x => new SelectListItem
          {
              Text = x.Nome,
              Value = x.Id.ToString()
          }).ToList();

            editPovoRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editPovoRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 100)).Where(x => editPovoRequest.SelectedContinentes.Contains(x.Continente?.Id.ToString()) && x.Continente != null).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editPovoRequest.Personagens = (await personagemRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editPovoRequest.Criaturas = (await criaturaRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editPovoRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPovoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
          .Select(x => new SelectListItem
          {
              Text = x.Titulo,
              Value = x.Id.ToString()
          }).ToList();

            return View(editPovoRequest);
        }

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

        //Maps Mundos from Selected mundo
        var selectedMundoId = editPovoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                povo.Mundo = selectedMundo;
            }
        }
        //Maps Continents from Selected continent
        var selectedContinentes = new List<Continente>();
        foreach (var selectedContinenteId in editPovoRequest.SelectedContinentes)
        {
            if (!string.IsNullOrEmpty(selectedContinenteId))
            {
                var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
                var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid, 1, 10);

                if (existingContinente != null)
                {
                    selectedContinentes.Add(existingContinente);
                }
            }
        }
        //Maping Continentes back to domain modal
        povo.Continentes = selectedContinentes;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();

        foreach (var selectedRegiaoId in editPovoRequest.SelectedRegioes)
        {
            if (!string.IsNullOrEmpty(selectedRegiaoId))
            {
                var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
                var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid, 1, 10);

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

        foreach (var selectedPersonagemId in editPovoRequest.SelectedPersonagens)
        {
            if (!string.IsNullOrEmpty(selectedPersonagemId))
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid, 1, 10);

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

        foreach (var selectedCriaturaId in editPovoRequest.SelectedCriaturas)
        {
            if (!string.IsNullOrEmpty(selectedCriaturaId))
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid, 1, 10);

                if (existingCriatura != null)
                {
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
        //Maping Criaturas back to domain modal
        povo.Criaturas = selectedCriaturas;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in editPovoRequest.SelectedContos)
        {
            if (!string.IsNullOrEmpty(selectedContoId))
            {
                var selectedContoIdAsGuid = Guid.Parse(selectedContoId);
                var existingConto = await contoRepository.GetAsync(selectedContoIdAsGuid, 1, 10);

                if (existingConto != null)
                {
                    selectedContos.Add(existingConto);
                }
            }
        }
        //Maping Contos back to domain modal
        povo.Contos = selectedContos;

        var updatedPovo = await povoRepository.UpdateAsync(povo, 1, 10);

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

    [HttpPost]
    [ValidateAntiForgeryToken]
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

    private async Task ValidateAddPovoRequest(AddPovoRequest addPovoRequest)
    {
        bool urlHandleExists = await povoRepository.UrlHandleExists(addPovoRequest.UrlHandle);

        if (urlHandleExists)
        {
            ModelState.AddModelError("UrlHandle", "This URL Handle already exists");
        }
    }

    private async Task ValidateEditPovoRequest(EditPovoRequest editPovoRequest)
    {
        var povo = await povoRepository.GetAsync(editPovoRequest.Id, 1, 10);

        bool urlHandleExists = await povoRepository.UrlHandleExists(editPovoRequest.UrlHandle);

        if (urlHandleExists && povo?.UrlHandle != editPovoRequest.UrlHandle)
        {
            ModelState.AddModelError("UrlHandle", "This URL Handle already exists");
        }
    }
}