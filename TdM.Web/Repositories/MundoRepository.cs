﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class MundoRepository : IMundoRepository
{
    private readonly TavernaDbContext tavernaDbContext;
    private readonly IMemoryCache cache;

    public MundoRepository(TavernaDbContext tavernaDbContext, IMemoryCache cache)
    {
        this.tavernaDbContext = tavernaDbContext;
        this.cache = cache;
    }
    public async Task<Mundo> AddAsync(Mundo mundo)
    {
        await tavernaDbContext.Mundos.AddAsync(mundo);
        await tavernaDbContext.SaveChangesAsync();
        return mundo;
    }

    public async Task<Mundo?> DeleteAsync(Guid id)
    {
        var existingMundo = await tavernaDbContext.Mundos.FindAsync(id);

        if (existingMundo != null)
        {
            tavernaDbContext.Mundos.Remove(existingMundo);
            await tavernaDbContext.SaveChangesAsync();
            return existingMundo;
        }
        return null;
    }

    public async Task<IEnumerable<Mundo>> GetAllAsync(int page, int pageSize)
    {
        string cacheKey = $"MundoRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Mundo>? result))
        {
            result = await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
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

    public async Task<Mundo?> GetAsync(Guid id, int page, int pageSize)
    {
        string cacheKey = $"MundoRepository.GetAsync_{id}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Mundo? result))
        {
            result = await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
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

    public async Task<Mundo?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        string cacheKey = $"MundoRepository.GetByUrlHandleAsync_{urlHandle}_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out Mundo? result))
        {
            result = await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
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

    public async Task<Mundo?> UpdateAsync(Mundo mundo, int page, int pageSize)
    {
        string cacheKey = $"MundoRepository.GetByUrlHandleAsync_{mundo.UrlHandle}_{page}_{pageSize}";

        var existingMundo = await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .FirstOrDefaultAsync(x => x.Id == mundo.Id);

        if (existingMundo != null)
        {
            existingMundo.Nome = mundo.Nome;
            existingMundo.CurtaDescricao = mundo.CurtaDescricao;
            existingMundo.Descricao = mundo.Descricao;
            existingMundo.Autor = mundo.Autor;
            existingMundo.ImgBox = mundo.ImgBox;
            existingMundo.PublishedDate = mundo.PublishedDate;
            existingMundo.UrlHandle = mundo.UrlHandle;
            existingMundo.Visible = mundo.Visible;
            existingMundo.Continentes = mundo.Continentes;
            existingMundo.Regioes = mundo.Regioes;
            existingMundo.Personagens = mundo.Personagens;
            existingMundo.Criaturas = mundo.Criaturas;
            existingMundo.Povos = mundo.Povos;
            existingMundo.Contos = mundo.Contos;
            await tavernaDbContext.SaveChangesAsync();

            cache.Remove(cacheKey);
            return existingMundo;
        }
        return null;
    }
}
