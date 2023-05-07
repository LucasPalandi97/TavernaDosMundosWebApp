using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
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

    public AdminContinentesController(IMundoRepository mundoRepository, IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository)
    {
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
    }

    public async Task<IActionResult> ListContinentesByMundo(Guid id)
    {
        IEnumerable<Continente> continentes;
        continentes = await continenteRepository.GetAllByMundoAsync(id, 1, 10);
        var selectListItems = continentes.Select(x => new SelectListItem
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
        var model = new AddContinenteRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            Regioes = regioes.Where(r => r.Mundo == null || r.Continente == null).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddContinenteRequest addContinenteRequest)
    {
        if (!ModelState.IsValid)
        {
            addContinenteRequest.Mundos = (await mundoRepository.GetAllAsync(1, 10))
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            addContinenteRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 10)).Where(r => r.Mundo == null || r.Continente == null)
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
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid,1 , 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Continentes back to domain modal
                continente.Mundo = selectedMundo;
            }
        }
        //Maps Regioes from Selected continent
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
                SelectedMundo = continente.Mundo?.Id.ToString(),
                Regioes = regioesDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegioes = continente.Regioes?.Select(x => x.Id.ToString()).ToArray(),
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

            editContinenteRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 10)).Where(r => r.Mundo == null || r.Continente == null)
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