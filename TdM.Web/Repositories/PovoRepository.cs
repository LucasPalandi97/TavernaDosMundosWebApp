using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class PovoRepository : IPovoRepository
{
    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public PovoRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
    }

    public async Task<Povo> AddAsync(Povo povo)
    {
        await tavernaDbContext.AddAsync(povo);
        await tavernaDbContext.SaveChangesAsync();
        return povo;
    }

    public async Task<Povo?> DeleteAsync(Guid id)
    {

        var existingPovo = await tavernaDbContext.Povos.FindAsync(id);

        if (existingPovo != null)
        {
            tavernaDbContext.Povos.Remove(existingPovo);
            await tavernaDbContext.SaveChangesAsync();
            return existingPovo;
        }
        return null;
    }

    public async Task<IEnumerable<Povo>> GetAllAsync(int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Povo>? result))
        {
            result = await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
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

    public async Task<IEnumerable<Povo>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetAllByMundoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Povo>? result))
        {
            result = await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
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

    public async Task<IEnumerable<Povo>> GetAllByPersonagem(object selectedPersonagemIds, int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetAllByPersonagem_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Povo>? result))
        {
            if (selectedPersonagemIds is Guid)
            {
                result = await tavernaDbContext.Povos
                .Where(p => p.Personagens
                .Any(pp => pp.Id == (Guid)selectedPersonagemIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedPersonagemIds is List<Guid> selectedPersonagemIdsList)
            {
                result = await tavernaDbContext.Povos
                .Where(p => p.Personagens
                .Any(pp => selectedPersonagemIdsList.Contains(pp.Id)))
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

    public async Task<IEnumerable<Povo>> GetAllByRegiao(object selectedRegiaoIds, int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetAllByRegiao_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Povo>? result))
        {
            if (selectedRegiaoIds is Guid)
            {
                result = await tavernaDbContext.Povos
                .Where(p => p.Regioes
                .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedRegiaoIds is List<Guid> sselectedRegiaoIdsList)
            {
                result = await tavernaDbContext.Povos
                .Where(p => p.Regioes
                .Any(pp => sselectedRegiaoIdsList.Contains(pp.Id)))
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

    public async Task<Povo?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Povo? result))
        {
            result = await tavernaDbContext.Povos
             .Include(x => x.Continentes)
             .Include(x => x.Regioes)
             .Include(x => x.Personagens)
             .Include(x => x.Criaturas)
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

    public async Task<Povo?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Povo? result))
        {
            result = await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
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


    public async Task<Povo?> UpdateAsync(Povo povo, int page, int pageSize)
    {
        string cacheKey = $"PovoRepository.GetByUrlHandleAsync_{povo.UrlHandle}_{page}_{pageSize}";

        var existingPovo = await tavernaDbContext.Povos
             .Include(x => x.Continentes)
             .Include(x => x.Regioes)
             .Include(x => x.Personagens)
             .Include(x => x.Criaturas)
             .Include(x => x.Contos)
             .Include(x => x.Mundo)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .FirstOrDefaultAsync(x => x.Id == povo.Id);

        if (existingPovo != null)
        {
            existingPovo.Nome = povo.Nome;
            existingPovo.CurtaDescricao = povo.CurtaDescricao;
            existingPovo.Descricao = povo.Descricao;
            existingPovo.ImgCard = povo.ImgCard;
            existingPovo.ImgBox = povo.ImgBox;
            existingPovo.PublishedDate = povo.PublishedDate;
            existingPovo.UrlHandle = povo.UrlHandle;
            existingPovo.Visible = povo.Visible;
            existingPovo.Mundo = povo.Mundo;
            existingPovo.Continentes = povo.Continentes;
            existingPovo.Regioes = povo.Regioes;
            existingPovo.Personagens = povo.Personagens;
            existingPovo.Criaturas = povo.Criaturas;
            await tavernaDbContext.SaveChangesAsync();

            cache.Remove(cacheKey);
            return existingPovo;
        }
        return null;
    }
}
