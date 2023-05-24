using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Drawing;
using TdM.Database.Models.Domain;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;

namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPersonagensController : Controller
{
    private readonly IPersonagemRepository personagemRepository;
    private readonly IMundoRepository mundoRepository;
    private readonly IContinenteRepository continenteRepository;
    private readonly IRegiaoRepository regiaoRepository;
    private readonly IPovoRepository povoRepository;
    private readonly IContoRepository contoRepository;

    public AdminPersonagensController(IPersonagemRepository personagemRepository, IMundoRepository mundoRepository,
        IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository,
        IPovoRepository povoRepository, IContoRepository contoRepository)
    {
        this.personagemRepository = personagemRepository;
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
        var model = new AddPersonagemRequest
        {
            Mundos = mundos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPersonagemRequest addPersonagemRequest)
    {
        if (!ModelState.IsValid)
        {
            addPersonagemRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            addPersonagemRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPersonagemRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            addPersonagemRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 100)).Where(x => x.Continente?.Id.ToString() == addPersonagemRequest.SelectedContinente).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            addPersonagemRequest.Povos = (await povoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPersonagemRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            addPersonagemRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == addPersonagemRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
            .Select(x => new SelectListItem
            {
                Text = x.Titulo,
                Value = x.Id.ToString()
            }).ToList();

            return View(addPersonagemRequest);
        }

        //Map view model to domain model
        var personagem = new Personagem
        {
            Nome = addPersonagemRequest.Nome,
            Titulo = addPersonagemRequest.Titulo,
            Classe = addPersonagemRequest.Classe,
            Raca = addPersonagemRequest.Raca,
            CurtaDescricao = addPersonagemRequest.CurtaDescricao,
            Biografia = addPersonagemRequest.Biografia,
            ImgCard = addPersonagemRequest.ImgCard,
            ImgBox = addPersonagemRequest.ImgBox,
            PublishedDate = addPersonagemRequest.PublishedDate,
            UrlHandle = addPersonagemRequest.UrlHandle,
            Visible = addPersonagemRequest.Visible,
        };

        //Maps Mundos from Selected mundo
        var selectedMundoId = addPersonagemRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Regioes back to domain modal
                personagem.Mundo = selectedMundo;
            }
        }

        //Maps Continentes from Selected Continente
        var selectedContinenteId = addPersonagemRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid, 1, 10);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Regioes back to domain modal
                personagem.Continente = selectedContinente;
            }
        }

        //Maps Regioes from Selected Regiao
        var selectedRegiaoId = addPersonagemRequest.SelectedRegiao;
        if (selectedRegiaoId != null)
        {
            var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
            var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid, 1, 10);

            if (existingRegiao != null)
            {
                var selectedRegiao = existingRegiao;
                //Maping Regiaos back to domain modal
                personagem.Regiao = selectedRegiao;
            }
        }

        //Maps Povos from Selected Regiao
        var selectedPovos = new List<Povo>();
        foreach (var selectedPovoId in addPersonagemRequest.SelectedPovos)
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
        personagem.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in addPersonagemRequest.SelectedContos)
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
        personagem.Contos = selectedContos;

        await personagemRepository.AddAsync(personagem);

        return RedirectToAction("List");
    }
    public async Task<IActionResult> ListPersonagensByMundo(Guid id)
    {
        IEnumerable<Personagem> personagens = await personagemRepository.GetAllByMundoAsync(id, 1, 100);
        var sortedPersonagens = personagens.OrderBy(p => p.Nome);
        var selectListItems = sortedPersonagens.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    public async Task<IActionResult> ListPersonagensSemRegiaoByMundo(Guid id)
    {
        IEnumerable<Personagem> personagens = await personagemRepository.GetAllByMundoAsync(id, 1, 100);
        var sortedPersonagens = personagens.Where(x => x.Regiao == null).OrderBy(p => p.Nome);
        var selectListItems = sortedPersonagens.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }

    public async Task<IActionResult> ListPersonagensByRegiao(Guid id, List<Guid> selectedRegiaoIds = null)
    {
        IEnumerable<Personagem> personagens;
        if (selectedRegiaoIds == null)
        {
            personagens = await personagemRepository.GetAllByRegiaoAsync(id, 1, 100);
        }
        else
        {
            personagens = await personagemRepository.GetAllByRegiaoAsync(selectedRegiaoIds, 1, 100);
        }

        var orderedPersonagens = personagens.OrderBy(x => x.Nome);
        var selectListItems = orderedPersonagens.Select(x => new SelectListItem
        {
            Text = x.Nome,
            Value = x.Id.ToString()
        });

        return Json(selectListItems);
    }


    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the personagem
        var personagens = await personagemRepository.GetAllAsync(1, 100);
        return View(personagens);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var personagem = await personagemRepository.GetAsync(id, 1, 10);
        var mundosDomainModel = await mundoRepository.GetAllAsync(1, 100);
        var continenteDomainModel = await continenteRepository.GetAllAsync(1, 100);
        var regiaoDomainModel = await regiaoRepository.GetAllAsync(1, 100);
        var povosDomainModel = await povoRepository.GetAllAsync(1, 100);
        var contosDomainModel = await contoRepository.GetAllAsync(1, 100);

        if (personagem != null)
        {
            //Map the domain model into the view model
            var editPersonagemRequest = new EditPersonagemRequest
            {
                Id = personagem.Id,
                Nome = personagem.Nome,
                Titulo = personagem.Titulo,
                Classe = personagem.Classe,
                Raca = personagem.Raca,
                CurtaDescricao = personagem.CurtaDescricao,
                Biografia = personagem.Biografia,
                ImgCard = personagem.ImgCard,
                ImgBox = personagem.ImgBox,
                PublishedDate = personagem.PublishedDate,
                UrlHandle = personagem.UrlHandle,
                Visible = personagem.Visible,

                Mundos = mundosDomainModel.OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = personagem.Mundo?.Id.ToString(),
                Continentes = personagem.Mundo?.Continentes?.OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinente = personagem.Continente?.Id.ToString(),
                Regioes = personagem.Continente?.Regioes?.OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegiao = personagem.Regiao?.Id.ToString(),
                Povos = povosDomainModel.Where(x => x.Mundo == personagem.Mundo && x.Mundo != null).OrderBy(x => x.Nome)
                .Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedPovos = personagem.Povos?.Select(x => x.Id.ToString()).ToArray(),
                Contos = contosDomainModel.Where(x => x.Mundo == personagem.Mundo && x.Mundo != null).OrderBy(x => x.Titulo)
                .Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.Id.ToString()
                }),
                SelectedContos = personagem.Contos?.Select(x => x.Id.ToString()).ToArray()

            };

            return View(editPersonagemRequest);
        }
        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPersonagemRequest editPersonagemRequest)
    {

        if (!ModelState.IsValid)
        {
            editPersonagemRequest.Mundos = (await mundoRepository.GetAllAsync(1, 100)).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editPersonagemRequest.Continentes = (await continenteRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPersonagemRequest.SelectedMundo).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editPersonagemRequest.Regioes = (await regiaoRepository.GetAllAsync(1, 100)).Where(x => x.Continente?.Id.ToString() == editPersonagemRequest.SelectedContinente).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editPersonagemRequest.Povos = (await povoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPersonagemRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Nome)
            .Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToList();

            editPersonagemRequest.Contos = (await contoRepository.GetAllAsync(1, 100)).Where(x => x.Mundo?.Id.ToString() == editPersonagemRequest.SelectedMundo && x.Mundo != null).OrderBy(x => x.Titulo)
            .Select(x => new SelectListItem
            {
                Text = x.Titulo,
                Value = x.Id.ToString()
            }).ToList();

            return View(editPersonagemRequest);
        }


        var personagem = new Personagem
        {
            Id = editPersonagemRequest.Id,
            Nome = editPersonagemRequest.Nome,
            Titulo = editPersonagemRequest.Titulo,
            Classe = editPersonagemRequest.Classe,
            Raca = editPersonagemRequest.Raca,
            CurtaDescricao = editPersonagemRequest.CurtaDescricao,
            Biografia = editPersonagemRequest.Biografia,
            ImgCard = editPersonagemRequest.ImgCard,
            ImgBox = editPersonagemRequest.ImgBox,
            PublishedDate = editPersonagemRequest.PublishedDate,
            UrlHandle = editPersonagemRequest.UrlHandle,
            Visible = editPersonagemRequest.Visible,
        };

        //Maps Mundos from Selected mundo

        var selectedMundoId = editPersonagemRequest.SelectedMundo;
        if (selectedMundoId != null)
        {
            var selectedMundoIdAsGuid = Guid.Parse(selectedMundoId);
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid, 1, 10);

            if (existingMundo != null)
            {
                var selectedMundo = existingMundo;
                //Maping Regioes back to domain modal
                personagem.Mundo = selectedMundo;
            }
        }

        //Maps Continentes from Selected Continente

        var selectedContinenteId = editPersonagemRequest.SelectedContinente;
        if (selectedContinenteId != null)
        {
            var selectedContinenteIdAsGuid = Guid.Parse(selectedContinenteId);
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid, 1, 10);

            if (existingContinente != null)
            {
                var selectedContinente = existingContinente;
                //Maping Regioes back to domain modal
                personagem.Continente = selectedContinente;
            }
        }

        //Maps Regioes from Selected Regiao

        var selectedRegiaoId = editPersonagemRequest.SelectedRegiao;
        if (selectedRegiaoId != null)
        {
            var selectedRegiaoIdAsGuid = Guid.Parse(selectedRegiaoId);
            var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid, 1, 10);

            if (existingRegiao != null)
            {
                var selectedRegiao = existingRegiao;
                //Maping Regiaos back to domain modal
                personagem.Regiao = selectedRegiao;
            }
        }

        //Maps Povos from Selected Regiao
        var selectedPovos = new List<Povo>();
        foreach (var selectedPovoId in editPersonagemRequest.SelectedPovos)
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
        personagem.Povos = selectedPovos;

        //Maps Contos from Selected Contos
        var selectedContos = new List<Conto>();

        foreach (var selectedContoId in editPersonagemRequest.SelectedContos)
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
        personagem.Contos = selectedContos;

        var updatedPersonagem = await personagemRepository.UpdateAsync(personagem, 1, 10);

        if (updatedPersonagem != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        else
        {
            //Show error notification          
        }

        return RedirectToAction("Edit", new { id = editPersonagemRequest.Id });
    }

    public async Task<IActionResult> Delete(EditPersonagemRequest editPersonagemRequest)
    {
        var deletedPersonagem = await personagemRepository.DeleteAsync(editPersonagemRequest.Id);

        if (deletedPersonagem != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        //Show an error notification
        return RedirectToAction("Edit", new { Id = editPersonagemRequest.Id });
    }
}