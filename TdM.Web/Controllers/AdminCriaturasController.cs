using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Drawing;
using System.Linq;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCriaturasController : Controller
{
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IPovoRepository povoRepository;
    private readonly IContoRepository contoRepository;

    public AdminCriaturasController(ICriaturaRepository criaturaRepository, IMundoRepository mundoRepository,
         IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository, IPovoRepository povoRepository, IContoRepository contoRepository)
    {
        this.criaturaRepository = criaturaRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
        this.povoRepository = povoRepository;
        this.contoRepository = contoRepository;
    }


    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos = await mundoRepository.GetAllAsync(1, 100);
        var model = new AddCriaturaRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Add(AddCriaturaRequest addCriaturaRequest)
    {
        await ValidateAddCriaturaRequest(addCriaturaRequest);

        if (!ModelState.IsValid)
        {
            addCriaturaRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addCriaturaRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addCriaturaRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addCriaturaRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addCriaturaRequest.SelectedMundo && x.Continente != null).OrderBy(x => x.Nome)
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addCriaturaRequest.Povos = (await povoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addCriaturaRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
              .Select(x => new SelectListItem
              {
                  Text = x.Nome,
                  Value = x.Id.ToString()
              }).ToList();

            addCriaturaRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addCriaturaRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
             .Select(x => new SelectListItem
             {
                 Text = x.Titulo,
                 Value = x.Id.ToString()
             }).ToList();

            return View(addCriaturaRequest);
        }

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
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                criatura.Mundo = selectedMundo;
            }
        }

        //Maps Continents from Selected continent
        var selectedContinentes = new List<Continente>();
        foreach (var selectedContinenteId in addCriaturaRequest.SelectedContinentes)
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
        criatura.Continentes = selectedContinentes;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();

        foreach (var selectedRegiaoId in addCriaturaRequest.SelectedRegioes)
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
        criatura.Regioes = selectedRegioes;

        //Maps Povos from Selected Regiao
        var selectedPovos = new List<Povo>();

        foreach (var selectedPovoId in addCriaturaRequest.SelectedPovos)
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
        criatura.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in addCriaturaRequest.SelectedContos)
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
        criatura.Contos = selectedContos;

        await criaturaRepository.AddAsync(criatura);
        return RedirectToAction("List");
    }
    public async Task<IActionResult> ListCriaturasByMundo(Guid id)
    {
        IEnumerable<Criatura> criaturas;
        criaturas = await criaturaRepository.GetAllByMundoAsync(id, 1, 100);
        var orderedCriaturas = criaturas.OrderBy(x => x.Nome);
        var selectListItems = orderedCriaturas.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    public async Task<IActionResult> ListCriaturasByRegiao(Guid id, List<Guid> selectedRegiaoIds = null)
    {
        IEnumerable<Criatura> criaturas;
        if (selectedRegiaoIds == null)
        {
            criaturas = await criaturaRepository.GetAllByRegiaoAsync(id, 1, 100);
        }
        else
        {
            criaturas = await criaturaRepository.GetAllByRegiaoAsync(selectedRegiaoIds, 1, 100);
        }
        var selectListItems = criaturas.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the criatura
        var criaturas = await criaturaRepository.GetAllAsync(1, 100);
        return View(criaturas);
    }

    [HttpGet]
    public async Task<IActionResult> CreatureBuildModal(Guid criaturaId)
    {
        var criatura = await criaturaRepository.GetAsync(criaturaId, 1, 10);
        return PartialView("_CreatureBuildModal", criatura);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var criatura = await criaturaRepository.GetAsync(id, 1, 10);
        var mundosDomainModel = await mundoRepository.GetAllAsync(1, 100);
        var continentesDomainModel = await continenteRepository.GetAllAsync(1, 100);
        var regioesDomainModel = await regiaoRepository.GetAllAsync(1, 100);
        var povosDomainModel = await povoRepository.GetAllAsync(1, 100);
        var contosDomainModel = await contoRepository.GetAllAsync(1, 100);

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

                Mundos = mundosDomainModel.OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = criatura.Mundo?.Id.ToString(),
                Continentes = continentesDomainModel.Where(x => x.Mundo == criatura.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinentes = criatura.Continentes?.Select(x => x.Id.ToString()).ToArray(),
                Regioes = regioesDomainModel.Where(x => criatura.Continentes.SelectMany(c => c.Regioes).Contains(x))
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegioes = criatura.Regioes?.Select(x => x.Id.ToString()).ToArray(),
                Povos = povosDomainModel.Where(x => x.Mundo == criatura.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedPovos = criatura.Povos?.Select(x => x.Id.ToString()).ToArray(),
                Contos = contosDomainModel.Where(x => x.Mundo == criatura.Mundo && x.Mundo != null).OrderBy(x => x.Titulo)
                .Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.Id.ToString()
                }),
                SelectedContos = criatura.Contos?.Select(x => x.Id.ToString()).ToArray()
            };
            return View(editCriaturaRequest);
        }
        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCriaturaRequest editCriaturaRequest)
    {
        await ValidateEditCriaturaRequest(editCriaturaRequest);

        if (!ModelState.IsValid)
        {

            editCriaturaRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editCriaturaRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editCriaturaRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editCriaturaRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 100)).Where(x => editCriaturaRequest.SelectedContinentes.Contains(x.Continente?.Id.ToString()) && x.Continente != null).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editCriaturaRequest.Povos = (await povoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editCriaturaRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editCriaturaRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editCriaturaRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
           .Select(x => new SelectListItem
           {
               Text = x.Titulo,
               Value = x.Id.ToString()
           }).ToList();

            return View(editCriaturaRequest);
        }

        var criatura = new Criatura
        {
            Id = editCriaturaRequest.Id,
            Nome = editCriaturaRequest.Nome,
            Tipo = editCriaturaRequest.Tipo,
            CurtaDescricao = editCriaturaRequest.CurtaDescricao,
            Descricao = editCriaturaRequest.Descricao,
            ImgCard = editCriaturaRequest.ImgCard,
            ImgBox = editCriaturaRequest.ImgBox,
            PublishedDate = editCriaturaRequest.PublishedDate,
            UrlHandle = editCriaturaRequest.UrlHandle,
            Visible = editCriaturaRequest.Visible,
        };

        //Maps Mundos from Selected mundo
        var selectedMundoId = editCriaturaRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                criatura.Mundo = selectedMundo;
            }
        }

        //Maps Continents from Selected continent
        var selectedContinentes = new List<Continente>();
        foreach (var selectedContinenteId in editCriaturaRequest.SelectedContinentes)
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
        criatura.Continentes = selectedContinentes;

        //Maps Regioes from Selected continent
        var selectedRegioes = new List<Regiao>();
        foreach (var selectedRegiaoId in editCriaturaRequest.SelectedRegioes)
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
        criatura.Regioes = selectedRegioes;

        //Maps Povos from Selected Regiao
        var selectedPovos = new List<Povo>();
        foreach (var selectedPovoId in editCriaturaRequest.SelectedPovos)
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
        criatura.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in editCriaturaRequest.SelectedContos)
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
        criatura.Contos = selectedContos;

        //Submit information to repository
        var updatedCriatura = await criaturaRepository.UpdateAsync(criatura, 1, 10);

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
    private async Task ValidateAddCriaturaRequest(AddCriaturaRequest addCriaturaRequest)
    {
        bool urlHandleExists = await criaturaRepository.UrlHandleExists(addCriaturaRequest.UrlHandle);

        if (urlHandleExists)
        {
            ModelState.AddModelError("UrlHandle", "This URL Handle already exists");
        }
    }
    private async Task ValidateEditCriaturaRequest(EditCriaturaRequest editCriaturaRequest)
    {
        var criatura = await criaturaRepository.GetAsync(editCriaturaRequest.Id, 1, 10);

        bool urlHandleExists = await criaturaRepository.UrlHandleExists(editCriaturaRequest.UrlHandle);

        if (urlHandleExists && criatura?.UrlHandle != editCriaturaRequest.UrlHandle)
        {
            ModelState.AddModelError("UrlHandle", "This URL Handle already exists");
        }
    }
}