using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class CriaturaRepository : ICriaturaRepository
{

    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public CriaturaRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
    }
    private void InvalidateCache(Guid id)
    {
        string cacheKey = $"CriaturaRepository.GetAsync_{id}_1_10";
        cache.Remove(cacheKey);
        cache.Remove("CriaturaRepository.GetAllAsync_1_10");
        cache.Remove("CriaturaRepository.GetAllByMundoAsync_mundoId_1_10");
        cache.Remove("CriaturaRepository.GetAllByRegiaoAsync_selectedRegiaoIds_1_10");
        cache.Remove("CriaturaRepository.GetAsync_id_1_10");
        cache.Remove("CriaturaRepository.GetByUrlHandleAsync_urlHandle_1_10");
    }
    public async Task<Criatura> AddAsync(Criatura criatura)
    {
        await tavernaDbContext.AddAsync(criatura);
        await tavernaDbContext.SaveChangesAsync();

        InvalidateCache(criatura.Id);
        return criatura;
    }

    public async Task<Criatura?> DeleteAsync(Guid id)
    {
        var existingCriatura = await tavernaDbContext.Criaturas.FindAsync(id);

        if (existingCriatura != null)
        {
            tavernaDbContext.Criaturas.Remove(existingCriatura);
            await tavernaDbContext.SaveChangesAsync();

            InvalidateCache(existingCriatura.Id);
            return existingCriatura;
        }
        return null;
    }

    public async Task<IEnumerable<Criatura>> GetAllAsync(int page, int pageSize)
    {
        string cacheKey = $"CriaturaRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Criatura>? result))
        {
            result = await tavernaDbContext.Criaturas
           
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
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

    public async Task<IEnumerable<Criatura>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        string cacheKey = $"CriaturaRepository.GetAllByMundoAsync_{mundoId}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Criatura>? result))
        {
            result = await tavernaDbContext.Criaturas
           
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .Where(x => x.Mundo.Id == mundoId)          
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

    public async Task<Criatura?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"CriaturaRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Criatura? result))
        {
            result = await tavernaDbContext.Criaturas
           
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo) 
           .Where(x => x.Id == id)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .FirstOrDefaultAsync();

            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<IEnumerable<Criatura>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        string cacheKey = $"CriaturaRepository.GetAllByRegiaoAsync_{string.Join("-", selectedRegiaoIds)}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Criatura>? result))
        {
            if (selectedRegiaoIds is Guid)
            {
                result = await tavernaDbContext.Criaturas
                
                .Where(p => p.Regioes
                .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
            {
                result = await tavernaDbContext.Criaturas
                
                .Where(p => p.Regioes
                .Any(pp => selectedRegiaoIdsList.Contains(pp.Id)))
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

    public async Task<Criatura?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"CriaturaRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Criatura? result))
        {
            result = await tavernaDbContext.Criaturas
           
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .Where(x => x.UrlHandle == urlHandle)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .FirstOrDefaultAsync();

            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }

    public async Task<Criatura?> UpdateAsync(Criatura criatura, int page, int pageSize)
    {       
        var existingCriatura = await tavernaDbContext.Criaturas
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .Where(x => x.Id == criatura.Id)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .FirstOrDefaultAsync();

        if (existingCriatura != null)
        {
            existingCriatura.Nome = criatura.Nome;
            existingCriatura.Tipo = criatura.Tipo;
            existingCriatura.CurtaDescricao = criatura.CurtaDescricao;
            existingCriatura.Descricao = criatura.Descricao;
            existingCriatura.ImgCard = criatura.ImgCard;
            existingCriatura.ImgBox = criatura.ImgBox;
            existingCriatura.PublishedDate = criatura.PublishedDate;
            existingCriatura.UrlHandle = criatura.UrlHandle;
            existingCriatura.Visible = criatura.Visible;
            existingCriatura.Mundo = criatura.Mundo;
            existingCriatura.Continentes = criatura.Continentes;
            existingCriatura.Regioes = criatura.Regioes;
            existingCriatura.Povos = criatura.Povos;
            existingCriatura.Contos = criatura.Contos;
            await tavernaDbContext.SaveChangesAsync();

            InvalidateCache(criatura.Id);
            InvalidateCache(existingCriatura.Id);

            return existingCriatura;

        }
        return null;
    }
}
