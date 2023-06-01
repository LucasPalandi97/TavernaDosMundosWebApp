using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
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
    private readonly IPersonagemRepository personagemRepository;
    private readonly ICriaturaRepository criaturaRepository;
    private readonly IPovoRepository povoRepository;
    private readonly IContoRepository contoRepository;

    public AdminRegioesController(IRegiaoRepository regiaoRepository, IMundoRepository mundoRepository, IContinenteRepository continenteRepository,
       IPersonagemRepository personagemRepository, ICriaturaRepository criaturaRepository, IPovoRepository povoRepository, IContoRepository contoRepository)
    {
        this.regiaoRepository = regiaoRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.personagemRepository = personagemRepository;
        this.criaturaRepository = criaturaRepository;
        this.povoRepository = povoRepository;
        this.contoRepository = contoRepository;
    }

    public async Task<IActionResult> ListRegioesByMundo(Guid id)
    {
        IEnumerable<Regiao> regioes;
        regioes = await regiaoRepository.GetAllByMundoAsync(id, 1, 100);
        var orderedRegioes = regioes.OrderBy(x => x.Nome);
        var selectListItems = orderedRegioes.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    public async Task<IActionResult> ListRegioesSemContinenteByMundo(Guid id)
    {
        IEnumerable<Regiao> regioes;
        regioes = await regiaoRepository.GetAllWithoutContinenteAsync(id, 1, 100);
        var orderedRegioes = regioes.Where(x => x.Continente == null).OrderBy(x => x.Nome);
        var selectListItems = orderedRegioes.Select(x => new SelectListItem
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
        var model = new AddRegiaoRequest
        {
            Mundos = mundos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddRegiaoRequest addRegiaoRequest)
    {
        await ValidateAddRegiaoRequest(addRegiaoRequest);

        if (!ModelState.IsValid)
        {
            addRegiaoRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            addRegiaoRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            addRegiaoRequest.Personagens = (await personagemRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addRegiaoRequest.SelectedMundo && x.Mundo != null && x.Regiao?.Id == null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            addRegiaoRequest.Criaturas = (await criaturaRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            addRegiaoRequest.Povos = (await povoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            addRegiaoRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
           .Select(x => new SelectListItem
           {
               Text = x.Titulo,
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

        //Maps Mundo from Selected Mundo
        var selectedMundoId = addRegiaoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Regioes back to domain modal
                regiao.Mundo = selectedMundo;
            }
        }
        //Maps Continente from Selected Continente
        var selectedContinenteId = addRegiaoRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid, 1, 10);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Regioes back to domain modal
                regiao.Continente = selectedContinente;
            }
        }

        //Maps Personagens from Selected Personagens and set their Continent
        var selectedPersonagens = new List<Personagem>();

        foreach (var selectedPersonagemId in addRegiaoRequest.SelectedPersonagens)
        {
            if (!string.IsNullOrEmpty(selectedPersonagemId))
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid, 1, 10);

                if (existingPersonagem != null)
                {
                    existingPersonagem.Continente = regiao.Continente;
                    await personagemRepository.UpdateAsync(existingPersonagem, 1, 10);
                    selectedPersonagens.Add(existingPersonagem);
                }
            }
        }
        //Maping Personagens back to domain modal
        regiao.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Criaturas
        var selectedCriaturas = new List<Criatura>();

        foreach (var selectedCriaturaId in addRegiaoRequest.SelectedCriaturas)
        {
            if (!string.IsNullOrEmpty(selectedCriaturaId))
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid, 1, 10);

                if (existingCriatura != null)
                {
                    existingCriatura.Continentes?.Add(regiao.Continente);
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
        //Maping Criaturas back to domain modal
        regiao.Criaturas = selectedCriaturas;


        //Maps Povos from Selected Povos
        var selectedPovos = new List<Povo>();

        foreach (var selectedPovoId in addRegiaoRequest.SelectedPovos)
        {
            if (!string.IsNullOrEmpty(selectedPovoId))
            {
                var selectedPovoIdAsGuid = Guid.Parse(selectedPovoId);
                var existingPovo = await povoRepository.GetAsync(selectedPovoIdAsGuid, 1, 10);

                if (existingPovo != null)
                {
                    existingPovo.Continentes?.Add(regiao.Continente);
                    selectedPovos.Add(existingPovo);
                }
            }
        }
        //Maping Povos back to domain modal
        regiao.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in addRegiaoRequest.SelectedContos)
        {
            if (!string.IsNullOrEmpty(selectedContoId))
            {
                var selectedContoIdAsGuid = Guid.Parse(selectedContoId);
                var existingConto = await contoRepository.GetAsync(selectedContoIdAsGuid, 1, 10);

                if (existingConto != null)
                {
                    existingConto.Continentes?.Add(regiao.Continente);
                    selectedContos.Add(existingConto);
                }
            }
        }
        //Maping Contos back to domain modal
        regiao.Contos = selectedContos;

        await regiaoRepository.AddAsync(regiao);

        return RedirectToAction("List");
    }

    public async Task<IActionResult> ListRegioesByContinente(Guid id, List<Guid> selectedContinenteIds = null)
    {
        IEnumerable<Regiao> regioes;
        if (selectedContinenteIds == null)
        {
            regioes = await regiaoRepository.GetAllByContinenteAsync(id, 1, 10);
        }
        else
        {
            regioes = await regiaoRepository.GetAllByContinenteAsync(selectedContinenteIds, 1, 10);
        }
        var orderedRegioes = regioes.OrderBy(x => x.Nome);
        var selectListItems = orderedRegioes.Select(x => new SelectListItem
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
        var regioes = await regiaoRepository.GetAllAsync(1, 100);
        return View(regioes);
    }

    [HttpGet]
    public async Task<IActionResult> RegionBuildModal(Guid regiaoId)
    {
        var regiao = await regiaoRepository.GetAsync(regiaoId, 1, 10);
        return PartialView("_RegionBuildModal", regiao);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var regiao = await regiaoRepository.GetAsync(id, 1, 100);
        var mundosDomainModel = await mundoRepository.GetAllAsync(1, 100);
        var continenteDomainModel = await continenteRepository.GetAllAsync(1, 100);
        var personagensDomainModel = await personagemRepository.GetAllAsync(1, 100);
        var criaturasDomainModel = await criaturaRepository.GetAllAsync(1, 100);
        var povosDomainModel = await povoRepository.GetAllAsync(1, 100);
        var contosDomainModel = await contoRepository.GetAllAsync(1, 100);
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

                Mundos = mundosDomainModel.OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = regiao.Mundo?.Id.ToString(),
                Continentes = regiao.Mundo?.Continentes?.OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinente = regiao.Continente?.Id.ToString(),
                Personagens = personagensDomainModel.Where(x => x.Mundo == regiao.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(c => new SelectListItem
                {
                    Text = c.Nome,
                    Value = c.Id.ToString()
                }),
                SelectedPersonagens = regiao.Personagens?.Select(c => c.Id.ToString()).ToArray(),
                Criaturas = criaturasDomainModel.Where(x => x.Mundo == regiao.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(c => new SelectListItem
                {
                    Text = c.Nome,
                    Value = c.Id.ToString()
                }),
                SelectedCriaturas = regiao.Criaturas?.Select(c => c.Id.ToString()).ToArray(),
                Povos = povosDomainModel.Where(x => x.Mundo == regiao.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedPovos = regiao.Povos?.Select(x => x.Id.ToString()).ToArray(),
                Contos = contosDomainModel.Where(x => x.Mundo == regiao.Mundo && x.Mundo != null).OrderBy(x => x.Titulo)
                .Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.Id.ToString()
                }),
                SelectedContos = regiao.Contos?.Select(x => x.Id.ToString()).ToArray()
            };
            return View(editRegiaoRequest);
        }
        return View(null);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditRegiaoRequest editRegiaoRequest)
    {
        await ValidateEditRegiaoRequest(editRegiaoRequest);

        if (!ModelState.IsValid)
        {
            editRegiaoRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editRegiaoRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editRegiaoRequest.Personagens = (await personagemRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editRegiaoRequest.Criaturas = (await criaturaRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editRegiaoRequest.Povos = (await povoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
           .Select(x => new SelectListItem
           {
               Text = x.Nome,
               Value = x.Id.ToString()
           }).ToList();

            editRegiaoRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editRegiaoRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
           .Select(x => new SelectListItem
           {
               Text = x.Titulo,
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

        //Maps Mundo from Selected Mundo
        var selectedMundoId = editRegiaoRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Regioes back to domain modal
                regiao.Mundo = selectedMundo;
            }
        }

        //Map Continente from Selected Continente
        var selectedContinenteId = editRegiaoRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid, 1, 10);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Regioes back to domain modal
                regiao.Continente = selectedContinente;
            }
        }

        //Maps Personagens from Selected Personagens and set their Continent
        var selectedPersonagens = new List<Personagem>();

        foreach (var selectedPersonagemId in editRegiaoRequest.SelectedPersonagens)
        {
            if (!string.IsNullOrEmpty(selectedPersonagemId))
            {
                var selectedPersonagemIdAsGuid = Guid.Parse(selectedPersonagemId);
                var existingPersonagem = await personagemRepository.GetAsync(selectedPersonagemIdAsGuid, 1, 10);

                if (existingPersonagem != null)
                {
                    existingPersonagem.Continente = regiao.Continente;
                    await personagemRepository.UpdateAsync(existingPersonagem, 1, 10);
                    selectedPersonagens.Add(existingPersonagem);
                }
            }
        }

        //Maping Personagens back to domain modal
        regiao.Personagens = selectedPersonagens;

        //Maps Criaturas from Selected Criaturas
        var selectedCriaturas = new List<Criatura>();

        foreach (var selectedCriaturaId in editRegiaoRequest.SelectedCriaturas)
        {
            if (!string.IsNullOrEmpty(selectedCriaturaId))
            {
                var selectedCriaturaIdAsGuid = Guid.Parse(selectedCriaturaId);
                var existingCriatura = await criaturaRepository.GetAsync(selectedCriaturaIdAsGuid, 1, 10);

                if (existingCriatura != null)
                {
                    if (existingCriatura.Continentes == null)
                    {
                        existingCriatura.Continentes = new List<Continente>();
                    }
                    existingCriatura.Continentes?.Add(regiao.Continente);
                    await criaturaRepository.UpdateAsync(existingCriatura, 1, 10);
                    selectedCriaturas.Add(existingCriatura);
                }
            }
        }
        //Maping Criaturas back to domain modal
        regiao.Criaturas = selectedCriaturas;


        //Maps Povos from Selected Povos
        var selectedPovos = new List<Povo>();

        foreach (var selectedPovoId in editRegiaoRequest.SelectedPovos)
        {
            if (!string.IsNullOrEmpty(selectedPovoId))
            {
                var selectedPovoIdAsGuid = Guid.Parse(selectedPovoId);
                var existingPovo = await povoRepository.GetAsync(selectedPovoIdAsGuid, 1, 10);

                if (existingPovo != null)
                {
                    if (existingPovo.Continentes == null)
                    {
                        existingPovo.Continentes = new List<Continente>();
                    }
                    existingPovo.Continentes?.Add(regiao.Continente);
                    await povoRepository.UpdateAsync(existingPovo, 1, 10);
                    selectedPovos.Add(existingPovo);
                }
            }
        }
        //Maping Povos back to domain modal
        regiao.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in editRegiaoRequest.SelectedContos)
        {
            if (!string.IsNullOrEmpty(selectedContoId))
            {
                var selectedContoIdAsGuid = Guid.Parse(selectedContoId);
                var existingConto = await contoRepository.GetAsync(selectedContoIdAsGuid, 1, 10);

                if (existingConto != null)
                {
                    if (existingConto.Continentes == null)
                    {
                        existingConto.Continentes = new List<Continente>();
                    }
                    existingConto.Continentes?.Add(regiao.Continente);
                    await contoRepository.UpdateAsync(existingConto, 1, 10);
                    selectedContos.Add(existingConto);
                }
            }
        }
        //Maping Contos back to domain modal
        regiao.Contos = selectedContos;

        var updatedRegiao = await regiaoRepository.UpdateAsync(regiao, 1, 10);

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

    [HttpPost]
    [ValidateAntiForgeryToken]
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

    private async Task ValidateAddRegiaoRequest(AddRegiaoRequest addRegiaoRequest)
    {
        bool urlHandleExists = await regiaoRepository.UrlHandleExists(addRegiaoRequest.UrlHandle);

        if (urlHandleExists)
        {
            ModelState.AddModelError("UrlHandle", "This URL Handle already exists");
        }
    }

    private async Task ValidateEditRegiaoRequest(EditRegiaoRequest editRegiaoRequest)
    {
        var regiao = await regiaoRepository.GetAsync(editRegiaoRequest.Id, 1, 10);

        bool urlHandleExists = await regiaoRepository.UrlHandleExists(editRegiaoRequest.UrlHandle);

        if (urlHandleExists && regiao?.UrlHandle != editRegiaoRequest.UrlHandle)
        {
            ModelState.AddModelError("UrlHandle", "This URL Handle already exists");
        }
    }
}