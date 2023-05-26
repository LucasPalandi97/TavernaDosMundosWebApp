using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Mundo>> GetAllAsync(int page, int pageSize)
    {
        return await tavernaDbContext.Mundos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mundo>> GetAllWithRelatedEntitiesAsync(int page, int pageSize)
    {
        return await tavernaDbContext.Mundos
           .Include(x => x.Personagens)
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Criaturas)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
    }

    public async Task<Mundo?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Where(x => x.Id == id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<Mundo?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Where(x => x.UrlHandle == urlHandle)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }
    public async Task<Mundo?> GetAllByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Mundos
            .Where(x => x.UrlHandle == urlHandle)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UrlHandleExists(string urlHandle)
    {
        bool urlHandleExists = await tavernaDbContext.Mundos.AnyAsync(m => m.UrlHandle == urlHandle);
        return urlHandleExists;
    }

    public async Task<Mundo?> UpdateAsync(Mundo mundo, int page, int pageSize)
    {
        var existingMundo = await tavernaDbContext.Mundos
            .Include(x => x.Personagens)
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Where(x => x.Id == mundo.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();

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
