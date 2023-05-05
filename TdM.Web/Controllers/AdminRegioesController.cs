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
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;

    public AdminRegioesController(IRegiaoRepository regiaoRepository, IMundoRepository mundoRepository, IContinenteRepository continenteRepository)
    {
        this.regiaoRepository = regiaoRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos = await mundoRepository.GetAllAsync();
        var model = new AddRegiaoRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddRegiaoRequest addRegiaoRequest)
    {

        if (!ModelState.IsValid)
        {
            addRegiaoRequest.Mundos = (await mundoRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            addRegiaoRequest.Continentes = (await continenteRepository.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            return View(addRegiaoRequest);
        }

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

        //Maps Mundos from Selected mundo

        var selectedMundoId = addRegiaoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Regioes back to domain modal
                regiao.Mundo = selectedMundo;
            }
        }
        //Maps Continentes from Selected Continente

        var selectedContinenteId = addRegiaoRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Regioes back to domain modal
                regiao.Continente = selectedContinente;
            }
        }
        await regiaoRepository.AddAsync(regiao);

        return RedirectToAction("List");
    }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    public async Task<IActionResult> ListRegioesByContinente(Guid id, List<Guid> selectedContinenteIds = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    {
        IEnumerable<Regiao> regioes;
        if (selectedContinenteIds == null)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            regioes = await regiaoRepository.GetRegioesByContinenteAsync(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        else
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            regioes = await regiaoRepository.GetRegioesByContinenteAsync(selectedContinenteIds);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        var selectListItems = regioes.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the regiao
        var regioes = await regiaoRepository.GetAllAsync();
        return View(regioes);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var regiao = await regiaoRepository.GetAsync(id);
        var mundosDomainModel = await mundoRepository.GetAllAsync();
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
                Mundos = mundosDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = regiao.Mundo?.Id.ToString(),
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
        if (!ModelState.IsValid)
        {
            editRegiaoRequest.Mundos = (await mundoRepository.GetAllAsync())
               .Select(x => new SelectListItem
               {
                   Text = x.Nome,
                   Value = x.Id.ToString()
               }).ToList();

            editRegiaoRequest.Continentes = (await continenteRepository.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }).ToList();

            return View(editRegiaoRequest);
        }

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

        //Maps Mundos from Selected mundo

        var selectedMundoId = editRegiaoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Regioes back to domain modal
                regiao.Mundo = selectedMundo;
            }
        }

        //Map Continente into domain model
        var selectedContinenteId = editRegiaoRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Regioes back to domain modal
                regiao.Continente = selectedContinente;
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