﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminMundosController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IPovoRepository povoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IPersonagemRepository personagemRepository;
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IContoRepository contoRepository;

    public AdminMundosController(IMundoRepository mundoRepository, IPovoRepository povoRepository
        , IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository
        , IPersonagemRepository personagemRepository, ICriaturaRepository criaturaRepository
        , IContoRepository contoRepository)
    {
        this.mundoRepository = mundoRepository;
        this.povoRepository = povoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
        this.personagemRepository = personagemRepository;
        this.criaturaRepository = criaturaRepository;
        this.contoRepository = contoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get continente from repository
        var continente = await continenteRepository.GetAllAsync();
        var regiao = await regiaoRepository.GetAllAsync();
        var personagem = await personagemRepository.GetAllAsync();
        var criatura = await criaturaRepository.GetAllAsync();
        var povo = await povoRepository.GetAllAsync();
        var conto = await contoRepository.GetAllAsync();

        var model = new AddMundoRequest
        {
            Continentes = continente.Where(c => c.Mundo == null).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString(), }),
            Regioes = regiao.Where(r => r.Mundo == null).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            Personagens = personagem.Where(pe => pe.Mundo == null).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            Criaturas = criatura.Where(cr => cr.Mundo == null).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            Povos = povo.Where(po => po.Mundo == null).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            Contos = conto.Where(co => co.Mundo == null).Select(x => new SelectListItem { Text = x.Titulo, Value = x.Id.ToString() }),
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Add(AddMundoRequest addMundoRequest)
    {
        if (!ModelState.IsValid)
        {
            addMundoRequest.Continentes = (await continenteRepository.GetAllAsync()).Where(c => c.Mundo == null)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addMundoRequest.Regioes = (await regiaoRepository.GetAllAsync()).Where(r => r.Mundo == null)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addMundoRequest.Personagens = (await personagemRepository.GetAllAsync()).Where(pe => pe.Mundo == null)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addMundoRequest.Criaturas = (await criaturaRepository.GetAllAsync()).Where(cr => cr.Mundo == null)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addMundoRequest.Povos = (await povoRepository.GetAllAsync()).Where(po => po.Mundo == null)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addMundoRequest.Contos = (await contoRepository.GetAllAsync()).Where(co => co.Mundo == null)
              .Select(x => new SelectListItem
              {
                  Text = x.Titulo,
                  Value = x.Id.ToString()
              }).ToList();

            return View(addMundoRequest);
        }

        //Maping AddMundoRequest to Mundo domain model
        var mundo = new Mundo
        {
            Nome = addMundoRequest.Nome,
            Descricao = addMundoRequest.Descricao,
            CurtaDescricao = addMundoRequest.CurtaDescricao,
            Autor = addMundoRequest.Autor,
            ImgBox = addMundoRequest.ImgBox,
            PublishedDate = addMundoRequest.PublishedDate,
            UrlHandle = addMundoRequest.UrlHandle,
            Visible = addMundoRequest.Visible,
        };

        //Maps Continents from Selected Continents
        var selectedContinentes = new List<Continente>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedContinenteId in addMundoRequest.SelectedContinentes)
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
        mundo.Continentes = selectedContinentes;

        //Maps Regioes from Selected Regioes
        var selectedRegioes = new List<Regiao>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedRegiaoId in addMundoRequest.SelectedRegioes)
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
        mundo.Regioes = selectedRegioes;

        //Maps Personagens from Selected Personagens
        var selectedPersonagens = new List<Personagem>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPersonagemId in addMundoRequest.SelectedPersonagens)
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
        mundo.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Criaturas
        var selectedCriaturas = new List<Criatura>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedCriaturaId in addMundoRequest.SelectedCriaturas)
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
        mundo.Criaturas = selectedCriaturas;

        //Maping Povos from Selected Povos
        var selectedPovos = new List<Povo>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPovoId in addMundoRequest.SelectedPovos)
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
        mundo.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedContoId in addMundoRequest.SelectedContos)
        {
            if (!string.IsNullOrEmpty(selectedContoId))
            {
                var selectedContoIdAsGuid = Guid.Parse(selectedContoId);
                var existingConto = await contoRepository.GetAsync(selectedContoIdAsGuid);

                if (existingConto != null)
                {
                    selectedContos.Add(existingConto);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Contos back to domain modal
        mundo.Contos = selectedContos;

        await mundoRepository.AddAsync(mundo);

        return RedirectToAction("List");
    }

    [HttpGet]
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
        var continentesDomainModel = await continenteRepository.GetAllAsync();
        var regioesDomainModel = await regiaoRepository.GetAllAsync();
        var personagensDomainModel = await personagemRepository.GetAllAsync();
        var criaturasDomainModel = await criaturaRepository.GetAllAsync();
        var povosDomainModel = await povoRepository.GetAllAsync();
        var contosDomainModel = await contoRepository.GetAllAsync();

        if (mundo != null)
        {
            //Map the domain model into the view model
#pragma warning disable CS8604 // Possible null reference argument.
            var editMundoRequest = new EditMundoRequest
            {
                Id = mundo.Id,
                Nome = mundo.Nome,
                CurtaDescricao = mundo.CurtaDescricao,
                Descricao = mundo.Descricao,
                Autor = mundo.Autor,
                ImgBox = mundo.ImgBox,
                PublishedDate = mundo.PublishedDate,
                UrlHandle = mundo.UrlHandle,
                Visible = mundo.Visible,
                Continentes = continentesDomainModel.Where(x => x.Mundo == mundo || x.Mundo == null).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinentes = mundo.Continentes.Select(x => x.Id.ToString()).ToArray(),
                Regioes = regioesDomainModel.Where(x => x.Mundo == mundo || x.Mundo == null).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegioes = mundo.Regioes.Select(x => x.Id.ToString()).ToArray(),
                Personagens = personagensDomainModel.Where(x => x.Mundo == mundo || x.Mundo == null).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedPersonagens = mundo.Personagens.Select(x => x.Id.ToString()).ToArray(),
                Criaturas = criaturasDomainModel.Where(x => x.Mundo == mundo || x.Mundo == null).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedCriaturas = mundo.Criaturas.Select(x => x.Id.ToString()).ToArray(),
                Povos = povosDomainModel.Where(x => x.Mundo == mundo || x.Mundo == null).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList(),
                SelectedPovos = mundo.Povos.Where(x => x.Mundo == mundo).Select(x => x.Id.ToString()).ToArray(),
                Contos = contosDomainModel.Where(x => x.Mundo == mundo || x.Mundo == null).Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.Id.ToString()
                }),
                SelectedContos = mundo.Contos.Where(x => x.Mundo == mundo).Select(x => x.Id.ToString()).ToArray()
            };
#pragma warning restore CS8604 // Possible null reference argument.
            return View(editMundoRequest);
        }
        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditMundoRequest editMundoRequest)
    {
        if (!ModelState.IsValid)
        {
            editMundoRequest.Continentes = (await continenteRepository.GetAllAsync())
             .Where(x => x.Mundo?.Id == editMundoRequest.Id || x.Mundo == null)
             .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            editMundoRequest.Regioes = (await regiaoRepository.GetAllAsync())
             .Where(x => x.Mundo?.Id == editMundoRequest.Id || x.Mundo == null)
             .Select(x => new SelectListItem
             {
                 Text = x.Nome,
                 Value = x.Id.ToString()
             }).ToList();

            editMundoRequest.Personagens = (await personagemRepository.GetAllAsync())
             .Where(x => x.Mundo?.Id == editMundoRequest.Id || x.Mundo == null)
             .Select(x => new SelectListItem
             {
                 Text = x.Nome,
                 Value = x.Id.ToString()
             }).ToList();

            editMundoRequest.Criaturas = (await criaturaRepository.GetAllAsync())
             .Where(x => x.Mundo?.Id == editMundoRequest.Id || x.Mundo == null)
             .Select(x => new SelectListItem
             {
                 Text = x.Nome,
                 Value = x.Id.ToString()
             }).ToList();

            editMundoRequest.Povos = (await povoRepository.GetAllAsync())
             .Where(x => x.Mundo?.Id == editMundoRequest.Id || x.Mundo == null)
             .Select(x => new SelectListItem
             {
                 Text = x.Nome,
                 Value = x.Id.ToString()
             }).ToList();

            editMundoRequest.Contos = (await contoRepository.GetAllAsync())
             .Where(x => x.Mundo?.Id == editMundoRequest.Id || x.Mundo == null)
             .Select(x => new SelectListItem
             {
                 Text = x.Titulo,
                 Value = x.Id.ToString()
             }).ToList();

            return View(editMundoRequest);
        }

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

        //Maps Continents from Selected Continents
        var selectedContinentes = new List<Continente>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedContinenteId in editMundoRequest.SelectedContinentes)
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
        mundo.Continentes = selectedContinentes;

        //Maps Regioes from Selected Regioes
        var selectedRegioes = new List<Regiao>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedRegiaoId in editMundoRequest.SelectedRegioes)
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
        mundo.Regioes = selectedRegioes;

        //Maps Personagens from Selected Personagens
        var selectedPersonagens = new List<Personagem>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPersonagemId in editMundoRequest.SelectedPersonagens)
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
        mundo.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Criaturas
        var selectedCriaturas = new List<Criatura>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedCriaturaId in editMundoRequest.SelectedCriaturas)
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
        mundo.Criaturas = selectedCriaturas;

        //Maping Povos from Selected Povos
        var selectedPovos = new List<Povo>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedPovoId in editMundoRequest.SelectedPovos)
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
        mundo.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var selectedContoId in editMundoRequest.SelectedContos)
        {
            if (!string.IsNullOrEmpty(selectedContoId))
            {
                var selectedContoIdAsGuid = Guid.Parse(selectedContoId);
                var existingConto = await contoRepository.GetAsync(selectedContoIdAsGuid);

                if (existingConto != null)
                {
                    selectedContos.Add(existingConto);
                }
            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //Maping Contos back to domain modal
        mundo.Contos = selectedContos;

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
    public async Task<IActionResult> Delete(EditMundoRequest editMundoRequest)
    {
        // Talk to repository to delete this mundo and continente
        var deletedMundo = await mundoRepository.DeleteAsync(editMundoRequest.Id);
        if (deletedMundo != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { editMundoRequest.Id });
    }
}