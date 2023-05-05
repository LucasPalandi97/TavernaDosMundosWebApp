using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class RegiaoRepository : IRegiaoRepository
{
    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public RegiaoRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
    }
    public async Task<Regiao> AddAsync(Regiao regiao)
    {
        await tavernaDbContext.AddAsync(regiao);
        await tavernaDbContext.SaveChangesAsync();
        return regiao;
    }

    public async Task<Regiao?> DeleteAsync(Guid id)
    {

        var existingRegiao = await tavernaDbContext.Regioes.FindAsync(id);

        if (existingRegiao != null)
        {
            tavernaDbContext.Regioes.Remove(existingRegiao);
            await tavernaDbContext.SaveChangesAsync();
            return existingRegiao;
        }
        return null;

    }

    public async Task<IEnumerable<Regiao>> GetAllAsync(int page, int pageSize)
    {
        string cacheKey = $"RegiaoRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Regiao>? result))
        {
            result = await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Mundo).ToListAsync();

            if (result != null && result.Any())
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<IEnumerable<Regiao>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        string cacheKey = $"RegiaoRepository.GetAllByMundoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Regiao>? result))
        {
            result = await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
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


    public async Task<Regiao?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"RegiaoRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Regiao? result))
        {
            result = await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
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

    public async Task<IEnumerable<Regiao>>? GetRegioesByContinenteAsync(object selectedContinenteIds, int page, int pageSize)
    {
        string cacheKey = $"RegiaoRepository.GetRegioesByContinenteAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Regiao>? result))
        {
            if (selectedContinenteIds is Guid)
            {
                result = await tavernaDbContext.Regioes.Where(r => r.Continente.Id == (Guid)selectedContinenteIds).ToListAsync();
            }
            else if (selectedContinenteIds is List<Guid> selectedContinenteIdsList)
            {
                result = await tavernaDbContext.Regioes.Where(r => selectedContinenteIdsList.Contains(r.Continente.Id)).ToListAsync();
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

    public async Task<Regiao?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"RegiaoRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Regiao? result))
        {
            result = await tavernaDbContext.Regioes.Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
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

    public async Task<Regiao?> UpdateAsync(Regiao regiao, int page, int pageSize)
    {
        string cacheKey = $"RegiaoRepository.GetByUrlHandleAsync_{regiao.UrlHandle}_{page}_{pageSize}";

        var existingRegiao = await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync(x => x.Id == regiao.Id);

        if (existingRegiao != null)
        {
            existingRegiao.Nome = regiao.Nome;
            existingRegiao.CurtaDescricao = regiao.CurtaDescricao;
            existingRegiao.Descricao = regiao.Descricao;
            existingRegiao.Simbolo = regiao.Simbolo;
            existingRegiao.ImgCard = regiao.ImgCard;
            existingRegiao.ImgBox = regiao.ImgBox;
            existingRegiao.PublishedDate = regiao.PublishedDate;
            existingRegiao.UrlHandle = regiao.UrlHandle;
            existingRegiao.Visible = regiao.Visible;
            existingRegiao.Continente = regiao.Continente;
            existingRegiao.Mundo = regiao.Continente?.Mundo;
            await tavernaDbContext.SaveChangesAsync();

            cache.Remove(cacheKey);
            return existingRegiao;
        }
        return null;
    }

}
