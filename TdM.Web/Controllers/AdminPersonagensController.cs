using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public AdminPersonagensController(IPersonagemRepository personagemRepository, IMundoRepository mundoRepository, IContinenteRepository continenteRepository, IRegiaoRepository regiaoRepository)
    {
        this.personagemRepository = personagemRepository;
        this.mundoRepository = mundoRepository;
        this.continenteRepository = continenteRepository;
        this.regiaoRepository = regiaoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //get mundos from repository
        var mundos = await mundoRepository.GetAllAsync();
        var model = new AddPersonagemRequest
        {
            Mundos = mundos.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() })
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPersonagemRequest addPersonagemRequest)
    {

        //Map view model to domain model

        var personagem = new Personagem
        {
            Nome = addPersonagemRequest.Nome,
            Titulo = addPersonagemRequest.Titulo,
            CurtaDescricao = addPersonagemRequest.CurtaDescricao,
            Biografia = addPersonagemRequest.Biografia,
            Classe = addPersonagemRequest.Classe,
            Raca = addPersonagemRequest.Raca,
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
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

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
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

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
            var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid);

            if (existingRegiao != null)
            {
                var selectedRegiao = existingRegiao;
                //Maping Regiaos back to domain modal
                personagem.Regiao = selectedRegiao;
            }
        }

        await personagemRepository.AddAsync(personagem);

        return RedirectToAction("List");
    }


    [HttpGet]
    public async Task<IActionResult> List()
    {
        // Use dbContext to read the personagem
        var personagens = await personagemRepository.GetAllAsync();
        return View(personagens);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        //Retrieve Result from repository
        var personagem = await personagemRepository.GetAsync(id);
        var mundosDomainModel = await mundoRepository.GetAllAsync();
        var continenteDomainModel = await continenteRepository.GetAllAsync();
        var regiaoDomainModel = await regiaoRepository.GetAllAsync();

        if (personagem != null)
        {
            //Map the domain model into the view model
            var editPersonagemRequest = new EditPersonagemRequest
            {
                Id = personagem.Id,
                Nome = personagem.Nome,
                Titulo = personagem.Titulo,
                CurtaDescricao = personagem.CurtaDescricao,
                Biografia = personagem.Biografia,
                Classe = personagem.Classe,
                Raca = personagem.Raca,
                ImgCard = personagem.ImgCard,
                ImgBox = personagem.ImgBox,
                PublishedDate = personagem.PublishedDate,
                UrlHandle = personagem.UrlHandle,
                Visible = personagem.Visible,
                Mundos = mundosDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedMundo = personagem.Mundo?.Id.ToString(),
                Continentes = continenteDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedContinente = personagem.Continente?.Id.ToString(),
                Regioes = regiaoDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString()
                }),
                SelectedRegiao = personagem.Regiao?.Id.ToString(),

            };

            return View(editPersonagemRequest);
        }
        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditPersonagemRequest editPersonagemRequest)
    {
        var personagem = new Personagem
        {

            Id = editPersonagemRequest.Id,
            Nome = editPersonagemRequest.Nome,
            Titulo = editPersonagemRequest.Titulo,
            CurtaDescricao = editPersonagemRequest.CurtaDescricao,
            Biografia = editPersonagemRequest.Biografia,
            Classe = editPersonagemRequest.Classe,
            Raca = editPersonagemRequest.Raca,
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
            var existingMundo = await mundoRepository.GetAsync(selectedMundoIdAsGuid);

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
            var existingContinente = await continenteRepository.GetAsync(selectedContinenteIdAsGuid);

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
            var existingRegiao = await regiaoRepository.GetAsync(selectedRegiaoIdAsGuid);

            if (existingRegiao != null)
            {
                var selectedRegiao = existingRegiao;
                //Maping Regiaos back to domain modal
                personagem.Regiao = selectedRegiao;
            }
        }

        var updatedPersonagem = await personagemRepository.UpdateAsync(personagem);

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