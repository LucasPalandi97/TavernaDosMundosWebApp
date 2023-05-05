using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class PersonagemRepository : IPersonagemRepository
{
    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public PersonagemRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
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
        string cacheKey = $"PersonagemRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Personagem>? result))
        {
            result = await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            if (result != null && result.Any())
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<IEnumerable<Personagem>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        string cacheKey = $"PersonagemRepository.GetAllByMundoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Personagem>? result))
        {
            result = await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            if (result != null && result.Any())
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<Personagem?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"PersonagemRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Personagem? result))
        {
            result = await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<IEnumerable<Personagem>>? GetPersonagensByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        string cacheKey = $"PersonagemRepository.GetPersonagensByRegiaoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Personagem>? result))
        {
            if (selectedRegiaoIds is Guid)
            {
                result = await tavernaDbContext.Personagens
                  .Where(r => r.Regiao.Id == (Guid)selectedRegiaoIds)
                  .AsNoTracking()
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
            }
            else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
            {
                result = await tavernaDbContext.Personagens
                  .Where(r => selectedRegiaoIdsList
                  .Contains(r.Regiao.Id))
                  .AsNoTracking()
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
            }
            else
            {
                throw new ArgumentException("Invalid argument type");
            }

            if (result != null && result.Any())
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<Personagem?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"PersonagemRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Personagem? result))
        {
            result = await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Mundo).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<Personagem?> UpdateAsync(Personagem personagem, int page, int pageSize)
    {
        string cacheKey = $"PersonagemRepository.GetByUrlHandleAsync_{personagem.UrlHandle}_{page}_{pageSize}";

        var existingPersonagem = await tavernaDbContext.Personagens
            .Include(x => x.Continente)
            .Include(x => x.Regiao)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
            existingPersonagem.Continente = personagem.Regiao?.Continente;
            existingPersonagem.Regiao = personagem.Regiao;
            existingPersonagem.Povos = personagem.Povos;
            existingPersonagem.Contos = personagem.Contos;
            existingPersonagem.Mundo = personagem.Regiao?.Mundo;
            await tavernaDbContext.SaveChangesAsync();

            cache.Remove(cacheKey);
            return existingPersonagem;
        }
        return null;
    }
}
