using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class ContoRepository : IContoRepository
{
    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public ContoRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
    }

    public async Task<Conto> AddAsync(Conto conto)
    {
        await tavernaDbContext.AddAsync(conto);
        await tavernaDbContext.SaveChangesAsync();
        return conto;
    }

    public async Task<Conto?> DeleteAsync(Guid id)
    {

        var existingConto = await tavernaDbContext.Contos.FindAsync(id);

        if (existingConto != null)
        {
            tavernaDbContext.Contos.Remove(existingConto);
            await tavernaDbContext.SaveChangesAsync();
            return existingConto;
        }
        return null;
    }

    public async Task<IEnumerable<Conto>> GetAllAsync(int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            result = await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
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

    public async Task<IEnumerable<Conto>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllByMundoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            result = await tavernaDbContext.Contos
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Personagens)
           .Include(x => x.Criaturas)
           .Include(x => x.Povos)
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

    public async Task<IEnumerable<Conto>> GetAllByContinenteAsync(object selectedContinenteIds, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllByContinenteAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            if (selectedContinenteIds is Guid)
            {
                result = await tavernaDbContext.Contos
                .Where(c => c.Continentes
                .Any(pp => pp.Id == (Guid)selectedContinenteIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedContinenteIds is List<Guid> selectedContinenteIdsList)
            {
                result = await tavernaDbContext.Contos
                .Where(c => c.Continentes
                .Any(pp => selectedContinenteIdsList.Contains(pp.Id)))
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

    public async Task<IEnumerable<Conto>> GetAllByRegionAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllByRegionAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            if (selectedRegiaoIds is Guid)
            {
                result = await tavernaDbContext.Contos
                .Where(r => r.Regioes
                .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedRegiaoIds is List<Guid> sselectedRegiaoIdsList)
            {
                result = await tavernaDbContext.Contos
                .Where(r => r.Regioes
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


    public async Task<IEnumerable<Conto>> GetAllByPersonagemAsync(object selectedPersonagemIds, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllByPersonagemAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            if (selectedPersonagemIds is Guid)
            {
                result = await tavernaDbContext.Contos
                .Where(p => p.Personagens
                .Any(pp => pp.Id == (Guid)selectedPersonagemIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedPersonagemIds is List<Guid> selectedPersonagemIdsList)
            {
                result = await tavernaDbContext.Contos
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

    public async Task<IEnumerable<Conto>> GetAllByCriaturaAsync(object selectedCriaturaIds, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllByCriaturaAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            if (selectedCriaturaIds is Guid)
            {
                result = await tavernaDbContext.Contos
                .Where(cr => cr.Criaturas
                .Any(pp => pp.Id == (Guid)selectedCriaturaIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedCriaturaIds is List<Guid> selectedCriaturaIdsList)
            {
                result = await tavernaDbContext.Contos
                .Where(cr => cr.Criaturas
                .Any(pp => selectedCriaturaIdsList.Contains(pp.Id)))
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

    public async Task<IEnumerable<Conto>> GetAllByPovoAsync(object selectedPovoIds, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAllByPovoAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Conto>? result))
        {
            if (selectedPovoIds is Guid)
            {
                result = await tavernaDbContext.Contos
                .Where(po => po.Povos
                .Any(pp => pp.Id == (Guid)selectedPovoIds))
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedPovoIds is List<Guid> selectedPovoIdsList)
            {
                result = await tavernaDbContext.Contos
                .Where(po => po.Povos
                .Any(pp => selectedPovoIdsList.Contains(pp.Id)))
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

    public async Task<Conto?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Conto? result))
        {
            result = await tavernaDbContext.Contos
          .Include(x => x.Continentes)
          .Include(x => x.Regioes)
          .Include(x => x.Personagens)
          .Include(x => x.Criaturas)
          .Include(x => x.Povos)
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

    public async Task<Conto?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Conto? result))
        {
            result = await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
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

    public async Task<Conto?> UpdateAsync(Conto conto, int page, int pageSize)
    {
        string cacheKey = $"ContoRepository.GetByUrlHandleAsync_{conto.UrlHandle}_{page}_{pageSize}";

        var existingConto = await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync(x => x.Id == conto.Id);

        if (existingConto != null)
        {
            existingConto.Titulo = conto.Titulo;
            existingConto.Corpo = conto.Corpo;
            existingConto.Autor = conto.Autor;
            existingConto.AudioDrama = conto.AudioDrama;
            existingConto.ImgCard = conto.ImgCard;
            existingConto.ImgBox = conto.ImgBox;
            existingConto.PublishedDate = conto.PublishedDate;
            existingConto.UrlHandle = conto.UrlHandle;
            existingConto.Visible = conto.Visible;
            existingConto.Mundo = conto.Mundo;
            existingConto.Continentes = conto.Continentes;
            existingConto.Regioes = conto.Regioes;
            existingConto.Personagens = conto.Personagens;
            existingConto.Criaturas = conto.Criaturas;
            existingConto.Povos = conto.Povos;
            await tavernaDbContext.SaveChangesAsync();

            cache.Remove(cacheKey);
            return existingConto;
        }
        return null;
    }
}


