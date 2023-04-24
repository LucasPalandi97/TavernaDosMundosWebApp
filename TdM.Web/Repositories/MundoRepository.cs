﻿using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class MundoRepository : IMundoRepository
{
    private readonly TavernaDbContext tavernaDbContext;

    public MundoRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;
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

    public async Task<IEnumerable<Mundo>> GetAllAsync()
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .ToListAsync();
    }

    public async Task<Mundo?> GetAsync(Guid id)
    {
        return await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Mundo?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<Mundo?> UpdateAsync(Mundo mundo)
    {
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
            return existingMundo;
        }

        return null;
    }
}
