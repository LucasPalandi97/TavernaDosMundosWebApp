using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

    public async Task<IEnumerable<Personagem>> GetAllAsync(int page, int pageSize)
    {
        return await tavernaDbContext.Personagens
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Personagem>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Personagens

         .Include(x => x.Continente)
         .Include(x => x.Regiao)
         .Include(x => x.Povos)
         .Include(x => x.Contos)
         .Include(x => x.Mundo)
         .Where(x => x.Mundo.Id == mundoId)
         .Skip((page - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();
    }

    public async Task<Personagem?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Personagens

        .Include(x => x.Continente)
        .Include(x => x.Regiao)
        .Include(x => x.Povos)
        .Include(x => x.Contos)
        .Include(x => x.Mundo)
        .Where(x => x.Id == id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Personagem>>? GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Personagens

               .Where(r => r.Regiao.Id == (Guid)selectedRegiaoIds)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
        {
            return await tavernaDbContext.Personagens

              .Where(r => selectedRegiaoIdsList
              .Contains(r.Regiao.Id))
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Personagem?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Personagens
        .Include(x => x.Continente)
        .Include(x => x.Regiao)
        .Include(x => x.Povos)
        .Include(x => x.Contos)
        .Where(x => x.UrlHandle == urlHandle)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Include(x => x.Mundo)
        .FirstOrDefaultAsync();
    }

    public async Task<Personagem?> UpdateAsync(Personagem personagem, int page, int pageSize)
    {
        var existingPersonagem = await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Id == personagem.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();

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
            existingPersonagem.Continente = personagem.Regiao?.Continente;
            existingPersonagem.Regiao = personagem.Regiao;
            existingPersonagem.Povos = personagem.Povos;
            existingPersonagem.Contos = personagem.Contos;
            existingPersonagem.Mundo = personagem.Regiao?.Mundo;
            await tavernaDbContext.SaveChangesAsync();

            return existingPersonagem;
        }
        return null;
    }
}
