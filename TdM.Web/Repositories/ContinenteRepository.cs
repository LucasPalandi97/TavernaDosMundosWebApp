using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace TdM.Web.Repositories;

public class ContinenteRepository : IContinenteRepository
{
    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public ContinenteRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
    }
    public async Task<Continente> AddAsync(Continente continente)
    {
        await tavernaDbContext.AddAsync(continente);
        await tavernaDbContext.SaveChangesAsync();
        return continente;
    }

    public async Task<Continente?> DeleteAsync(Guid id)
    {
        var existingContinente = await tavernaDbContext.Continentes.FindAsync(id);

        if (existingContinente != null)
        {
            tavernaDbContext.Continentes.Remove(existingContinente);
            await tavernaDbContext.SaveChangesAsync();
            return existingContinente;
        }
        return null;
    }

    public async Task<IEnumerable<Continente>> GetAllAsync(int page, int pageSize)
    {
        string cacheKey = $"ContinenteRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Continente>? result))
        {
            result = await tavernaDbContext.Continentes
                .Include(x => x.Regioes)
                .Include(x => x.Personagens)
                .Include(x => x.Criaturas)
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

    public async Task<IEnumerable<Continente>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        string cacheKey = $"ContinenteRepository.GetAllByMundoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Continente>? result))
        {
            result = await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
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

    public async Task<Continente?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"ContinenteRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Continente? result))
        {
            result = await tavernaDbContext.Continentes
                .Include(x => x.Regioes)
                .Include(x => x.Personagens)
                .Include(x => x.Criaturas)
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

    public async Task<IEnumerable<Continente>> GetContinentesByMundoAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"ContinenteRepository.GetContinentesByMundoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Continente>? result))
        {
            result = await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .AsNoTracking()
            .Where(c => c.Mundo.Id == id)
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

    public async Task<Continente?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"ContinenteRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Continente? result))
        {
            result = await tavernaDbContext.Continentes
                .Include(x => x.Regioes)
                .Include(x => x.Personagens)
                .Include(x => x.Criaturas)
                .Include(x => x.Povos)
                .Include(x => x.Contos)
                .Include(x => x.Mundo)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<Continente?> UpdateAsync(Continente continente, int page, int pageSize)
    {
        string cacheKey = $"ContinenteRepository.GetByUrlHandleAsync_{continente.UrlHandle}_{page}_{pageSize}";

        var existingContinente = await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync(x => x.Id == continente.Id);

        if (existingContinente != null)
        {
            existingContinente.Nome = continente.Nome;
            existingContinente.CurtaDescricao = continente.CurtaDescricao;
            existingContinente.Descricao = continente.Descricao;
            existingContinente.ImgCard = continente.ImgCard;
            existingContinente.ImgBox = continente.ImgBox;
            existingContinente.PublishedDate = continente.PublishedDate;
            existingContinente.UrlHandle = continente.UrlHandle;
            existingContinente.Visible = continente.Visible;
            existingContinente.Mundo = continente.Mundo;
            existingContinente.Regioes = continente.Regioes;
            await tavernaDbContext.SaveChangesAsync();

            cache.Remove(cacheKey);
            return existingContinente;
        }
        return null;
    }

}
