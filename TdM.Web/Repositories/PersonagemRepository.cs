using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class PersonagemRepository : IPersonagemRepository
{
    private readonly TavernaDbContext tavernaDbContext;

    public PersonagemRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;
    }
    public async Task<Personagem> AddAsync(Personagem personagem)
    {
        await tavernaDbContext.AddAsync(personagem);
        await tavernaDbContext.SaveChangesAsync();
        return personagem;
    }

    public async Task<Personagem?> DeleteAsync(Guid id)
    {

        var existingPersonagem = await tavernaDbContext.Personagens.FindAsync(id);

        if (existingPersonagem != null)
        {
            tavernaDbContext.Personagens.Remove(existingPersonagem);
            await tavernaDbContext.SaveChangesAsync();
            return existingPersonagem;
        }
        return null;

    }

    public async Task<IEnumerable<Personagem>> GetAllAsync()
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Personagens.Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Mundo)
            .Include(x => x.Povos).Include(x => x.Contos)
            .Include(x => x.Mundo).ToListAsync();
    }

    public async Task<IEnumerable<Personagem>> GetAllByMundoAsync(Guid mundoId)
    {
        return await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId)
            .ToListAsync();
    }

    public async Task<Personagem?> GetAsync(Guid id)
    {
        return await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Personagem>>? GetPersonagensByRegiaoAsync(object selectedRegiaoIds)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Personagens.Where(r => r.Regiao.Id == (Guid)selectedRegiaoIds).ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
        {
            return await tavernaDbContext.Personagens.Where(r => selectedRegiaoIdsList.Contains(r.Regiao.Id)).ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Personagem?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<Personagem?> UpdateAsync(Personagem personagem)
    {
        var existingPersonagem = await tavernaDbContext.Personagens.Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .FirstOrDefaultAsync(x => x.Id == personagem.Id);

        if (existingPersonagem != null)
        {
            existingPersonagem.Nome = personagem.Nome;
            existingPersonagem.Titulo = personagem.Titulo;
            existingPersonagem.Classe = personagem.Classe;
            existingPersonagem.Raca = personagem.Raca;
            existingPersonagem.CurtaDescricao = personagem.CurtaDescricao;
            existingPersonagem.Biografia = personagem.Biografia;
            existingPersonagem.ImgCard = personagem.ImgCard;
            existingPersonagem.ImgBox = personagem.ImgBox;
            existingPersonagem.PublishedDate = personagem.PublishedDate;
            existingPersonagem.UrlHandle = personagem.UrlHandle;
            existingPersonagem.Visible = personagem.Visible;
            existingPersonagem.Regiao = personagem.Regiao;
            existingPersonagem.Continente = personagem.Regiao?.Continente;
            existingPersonagem.Mundo = personagem.Regiao?.Mundo;
            await tavernaDbContext.SaveChangesAsync();

            return existingPersonagem;
        }

        return null;
    }
}
