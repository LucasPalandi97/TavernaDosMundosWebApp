using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class ContinenteRepository : IContinenteRepository
{
    private readonly TavernaDbContext tavernaDbContext;

    public ContinenteRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;
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
        return await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Continente>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Continentes
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Continente?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Id == id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<Continente?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.UrlHandle == urlHandle)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> UrlHandleExists(string urlHandle)
    {
        bool urlHandleExists = await tavernaDbContext.Continentes.AnyAsync(m => m.UrlHandle == urlHandle);
        return urlHandleExists;
    }
    public async Task<Continente?> UpdateAsync(Continente continente, int page, int pageSize)
    {
        var existingContinente = await tavernaDbContext.Continentes
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Id == continente.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();

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
            existingContinente.Criaturas = continente.Criaturas;
            existingContinente.Povos = continente.Povos;
            existingContinente.Contos = continente.Contos;
            await tavernaDbContext.SaveChangesAsync();

            return existingContinente;
        }
        return null;
    }
}
