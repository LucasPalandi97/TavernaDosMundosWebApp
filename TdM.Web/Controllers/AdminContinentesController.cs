using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminContinentesController : Controller
{
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IPovoRepository povoRepository;
    private readonly IContoRepository contoRepository;

    public AdminContinentesController(IMundoRepository mundoRepository, IContinenteRepository continenteRepository,
        IRegiaoRepository regiaoRepository, ICriaturaRepository criaturaRepository, IPovoRepository povoRepository,
        IContoRepository contoRepository)
    {
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
        this.criaturaRepository = criaturaRepository;
        this.povoRepository = povoRepository;
        this.contoRepository = contoRepository;
    }

    public async Task<IActionResult> ListContinentesByMundo(Guid id)
    {
        IEnumerable<Continente> continentes;
        continentes = await continenteRepository.GetAllByMundoAsync(id, 1, 10);
        var orderedContinentes = continentes.OrderBy(x => x.Nome);
        var selectListItems = orderedContinentes.Select(x => new SelectListItem
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
        var mundos = await mundoRepository.GetAllAsync(1, 10);
        var regioes = await regiaoRepository.GetAllAsync(1, 10);
        var criatura = await criaturaRepository.GetAllAsync(1, 10);
        var povo = await povoRepository.GetAllAsync(1, 10);
        var conto = await contoRepository.GetAllAsync(1, 10);
        var model = new AddContinenteRequest
        {
            Mundos = mundos.OrderBy(r => r.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            Regioes = regioes.Where(r => r.Mundo == null || r.Continente == null).OrderBy(r => r.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddContinenteRequest addContinenteRequest)
    {
        if (!ModelState.IsValid)
        {
            addContinenteRequest.Mundos = (await mundoRepository.GetAllAsync(1, 10)).OrderBy(r => r.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addContinenteRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 10)).Where(r => r.Mundo == null || r.Continente == null).OrderBy(r => r.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            return View(addContinenteRequest);
        }

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
        if (!string.IsNullOrEmpty(selectedMundoId))
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                continente.Mundo = selectedMundo;
            }
        }
        //Maps Regioes from Selected Regioes
        var selectedRegioes = new List<Regiao>();

        foreach (var selectedRegiaoId in addContinenteRequest.SelectedRegioes)
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
        continente.Regioes = selectedRegioes;

        //Maps Criaturas from Selected Criaturas
        var selectedCriaturas = new List<Criatura>();

        foreach (var selectedCriaturaId in addContinenteRequest.SelectedCriaturas)
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
        continente.Criaturas = selectedCriaturas;

        //Maping Povos from Selected Povos
        var selectedPovos = new List<Povo>();

        foreach (var selectedPovoId in addContinenteRequest.SelectedPovos)
        {
            if (!string.IsNullOrEmpty(selectedPovoId))
            {
                var selectedPovoIdAsGuid = Guid.Parse(selectedPovoId);
                var existingPovo = await povoRepository.GetAsync(selectedPovoIdAsGuid, 1, 10);

                if (existingPovo != null)
                {
                    selectedPovos.Add(existingPovo);
                }
            }
        }
        //Maping Povos back to domain modal
        continente.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in addContinenteRequest.SelectedContos)
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
        continente.Contos = selectedContos;

        await continenteRepository.AddAsync(continente);

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the continente
        var continentes = await continenteRepository.GetAllAsync(1, 10);
        return View(continentes);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var continente = await continenteRepository.GetAsync(id, 1, 10);
        var mundosDomainModel = await mundoRepository.GetAllAsync(1, 10);
        var regioesDomainModel = await regiaoRepository.GetAllAsync(1, 10);
        var criaturasDomainModel = await criaturaRepository.GetAllAsync(1, 10);
        var povosDomainModel = await povoRepository.GetAllAsync(1, 10);
        var contosDomainModel = await contoRepository.GetAllAsync(1, 10);

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
                Mundos = mundosDomainModel.OrderBy(x => x.Nome).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = continente.Mundo?.Id.ToString(),
                Regioes = regioesDomainModel.Where(x => x.Mundo == continente.Mundo || x.Mundo == null).OrderBy(x => x.Nome).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegioes = continente.Regioes?.Select(x => x.Id.ToString()).ToArray(),

                Criaturas = criaturasDomainModel.Where(x => x.Mundo == continente.Mundo).Select(c => new SelectListItem
                {
                    Text = c.Nome,
                    Value = c.Id.ToString()
                }),
                SelectedCriaturas = continente.Criaturas?.Select(c => c.Id.ToString()).ToArray(),           
                Povos = povosDomainModel.Where(x => x.Mundo == continente.Mundo).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedPovos = continente.Povos?.Select(x => x.Id.ToString()).ToArray(),
                Contos = povosDomainModel.Where(x => x.Mundo == continente.Mundo).Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContos = continente.Contos?.Select(x => x.Id.ToString()).ToArray()

            };
            return View(editContinenteRequest);
        }
        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditContinenteRequest editContinenteRequest)
    {
        if (!ModelState.IsValid)
        {
            editContinenteRequest.Mundos = (await mundoRepository.GetAllAsync(1, 10))
             .Select(x => new SelectListItem
             {
                 Text = x.Nome,
                 Value = x.Id.ToString()
             }).ToList();

            editContinenteRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 10))
             .Where(x => x.Continente?.Id == editContinenteRequest.Id || x.Continente == null)
             .Select(x => new SelectListItem
             {
                 Text = x.Nome,
                 Value = x.Id.ToString()
             }).ToList();

            return View(editContinenteRequest);
        }

        var continente = new Continente
        {
            Id = editContinenteRequest.Id,
            Nome = editContinenteRequest.Nome,
            CurtaDescricao = editContinenteRequest.CurtaDescricao,
            Descricao = editContinenteRequest.Descricao,
            ImgCard = editContinenteRequest.ImgCard,
            ImgBox = editContinenteRequest.ImgBox,
            PublishedDate = editContinenteRequest.PublishedDate,
            UrlHandle = editContinenteRequest.UrlHandle,
            Visible = editContinenteRequest.Visible,
        };

        //Map Mundo into domain model
        var selectedMundoId = editContinenteRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                continente.Mundo = selectedMundo;
            }
        }

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();

        foreach (var selectedRegiaoId in editContinenteRequest.SelectedRegioes)
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
        continente.Regioes = selectedRegioes;

        //Maps Criaturas from Selected Criaturas
        var selectedCriaturas = new List<Criatura>();

        foreach (var selectedCriaturaId in editContinenteRequest.SelectedCriaturas)
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
        continente.Criaturas = selectedCriaturas;

        //Maping Povos from Selected Povos
        var selectedPovos = new List<Povo>();

        foreach (var selectedPovoId in editContinenteRequest.SelectedPovos)
        {
            if (!string.IsNullOrEmpty(selectedPovoId))
            {
                var selectedPovoIdAsGuid = Guid.Parse(selectedPovoId);
                var existingPovo = await povoRepository.GetAsync(selectedPovoIdAsGuid, 1, 10);

                if (existingPovo != null)
                {
                    selectedPovos.Add(existingPovo);
                }
            }
        }
        //Maping Povos back to domain modal
        continente.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in editContinenteRequest.SelectedContos)
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
        continente.Contos = selectedContos;

        var updatedContinente = await continenteRepository.UpdateAsync(continente, 1, 10);

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