using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class ContoRepository : IContoRepository
{
    private readonly TavernaDbContext tavernaDbContext;

    public ContoRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;

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
        return await tavernaDbContext.Contos
             .Include(x => x.Mundo)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();
    }

    public async Task<IEnumerable<Conto>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Contos
           .Include(x => x.Mundo)
           .Where(x => x.Mundo.Id == mundoId)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
    }

    public async Task<IEnumerable<Conto>> GetAllByContinenteAsync(object selectedContinenteIds, int page, int pageSize)
    {
        if (selectedContinenteIds is Guid)
        {
            return await tavernaDbContext.Contos
                .Where(c => c.Continentes
                .Any(pp => pp.Id == (Guid)selectedContinenteIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (selectedContinenteIds is List<Guid> selectedContinenteIdsList)
        {
            return await tavernaDbContext.Contos
                .Where(c => c.Continentes
                .Any(pp => selectedContinenteIdsList.Contains(pp.Id)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Conto>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Contos

                .Where(r => r.Regioes
                .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
        {
            return await tavernaDbContext.Contos

                .Where(r => r.Regioes
                .Any(pp => selectedRegiaoIdsList.Contains(pp.Id)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Conto>> GetAllByPersonagemAsync(object selectedPersonagemIds, int page, int pageSize)
    {
        if (selectedPersonagemIds is Guid)
        {
            return await tavernaDbContext.Contos
                .Where(p => p.Personagens
                .Any(pp => pp.Id == (Guid)selectedPersonagemIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (selectedPersonagemIds is List<Guid> selectedPersonagemIdsList)
        {
            return await tavernaDbContext.Contos
                .Where(p => p.Personagens
                .Any(pp => selectedPersonagemIdsList.Contains(pp.Id)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Conto>> GetAllByCriaturaAsync(object selectedCriaturaIds, int page, int pageSize)
    {
        if (selectedCriaturaIds is Guid)
        {
            return await tavernaDbContext.Contos
                .Where(cr => cr.Criaturas
                .Any(pp => pp.Id == (Guid)selectedCriaturaIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (selectedCriaturaIds is List<Guid> selectedCriaturaIdsList)
        {
            return await tavernaDbContext.Contos
                .Where(cr => cr.Criaturas
                .Any(pp => selectedCriaturaIdsList.Contains(pp.Id)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Conto>> GetAllByPovoAsync(object selectedPovoIds, int page, int pageSize)
    {
        if (selectedPovoIds is Guid)
        {
            return await tavernaDbContext.Contos
                .Where(po => po.Povos
                .Any(pp => pp.Id == (Guid)selectedPovoIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (selectedPovoIds is List<Guid> selectedPovoIdsList)
        {
            return await tavernaDbContext.Contos
                .Where(po => po.Povos
                .Any(pp => selectedPovoIdsList.Contains(pp.Id)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Conto?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Contos
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Personagens)
           .Include(x => x.Criaturas)
           .Include(x => x.Povos)
           .Include(x => x.Mundo)
           .Where(x => x.Id == id)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .FirstOrDefaultAsync();
    }

    public async Task<Conto?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Mundo)
            .Where(x => x.UrlHandle == urlHandle)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> UrlHandleExists(string urlHandle)
    {
        bool urlHandleExists = await tavernaDbContext.Contos.AnyAsync(m => m.UrlHandle == urlHandle);
        return urlHandleExists;
    }
    public async Task<Conto?> UpdateAsync(Conto conto, int page, int pageSize)
    {
        var existingConto = await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Mundo)
            .Where(x => x.Id == conto.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();

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

            return existingConto;
        }
        return null;
    }
}


