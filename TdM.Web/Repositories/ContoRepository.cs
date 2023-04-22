using Microsoft.EntityFrameworkCore;
using System.Drawing;
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

    public async Task<IEnumerable<Conto>> GetAllAsync()
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Mundo).ToListAsync();
    }

    public async Task<IEnumerable<Conto>> GetAllByMundoAsync(Guid mundoId)
    {
        return await tavernaDbContext.Contos
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Personagens)
           .Include(x => x.Criaturas)
           .Include(x => x.Povos)
           .Include(x => x.Mundo)
           .Where(x => x.Mundo.Id == mundoId)
           .ToListAsync();
    }


    public async Task<IEnumerable<Conto>> GetAllByContinenteAsync(object selectedContinenteIds)
    {
        if (selectedContinenteIds is Guid)
        {
            return await tavernaDbContext.Contos
            .Where(c => c.Continentes
            .Any(pp => pp.Id == (Guid)selectedContinenteIds))
            .ToListAsync();
        }
        else if (selectedContinenteIds is List<Guid> selectedContinenteIdsList)
        {
            return await tavernaDbContext.Contos
            .Where(c => c.Continentes
            .Any(pp => selectedContinenteIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Conto>> GetAllByRegionAsync(object selectedRegiaoIds)
    {

        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Contos
            .Where(r => r.Regioes
            .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
            .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> sselectedRegiaoIdsList)
        {
            return await tavernaDbContext.Contos
            .Where(r => r.Regioes
            .Any(pp => sselectedRegiaoIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }


    public async Task<IEnumerable<Conto>> GetAllByPersonagemAsync(object selectedPersonagemIds)
    {
        if (selectedPersonagemIds is Guid)
        {
            return await tavernaDbContext.Contos
            .Where(p => p.Personagens
            .Any(pp => pp.Id == (Guid)selectedPersonagemIds))
            .ToListAsync();
        }
        else if (selectedPersonagemIds is List<Guid> selectedPersonagemIdsList)
        {
            return await tavernaDbContext.Contos
            .Where(p => p.Personagens
            .Any(pp => selectedPersonagemIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Conto>> GetAllByCriaturaAsync(object selectedCriaturaIds)
    {
        if (selectedCriaturaIds is Guid)
        {
            return await tavernaDbContext.Contos
            .Where(cr => cr.Criaturas
            .Any(pp => pp.Id == (Guid)selectedCriaturaIds))
            .ToListAsync();
        }
        else if (selectedCriaturaIds is List<Guid> selectedCriaturaIdsList)
        {
            return await tavernaDbContext.Contos
            .Where(cr => cr.Criaturas
            .Any(pp => selectedCriaturaIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    
    public async Task<IEnumerable<Conto>> GetAllByPovoAsync(object selectedPovoIds)
    {
        if (selectedPovoIds is Guid)
        {
            return await tavernaDbContext.Contos
            .Where(po => po.Povos
            .Any(pp => pp.Id == (Guid)selectedPovoIds))
            .ToListAsync();
        }
        else if (selectedPovoIds is List<Guid> selectedPovoIdsList)
        {
            return await tavernaDbContext.Contos
            .Where(po => po.Povos
            .Any(pp => selectedPovoIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }
    public async Task<Conto?> GetAsync(Guid id)
    {
        return await tavernaDbContext.Contos
          .Include(x => x.Continentes)
          .Include(x => x.Regioes)
          .Include(x => x.Personagens)
          .Include(x => x.Criaturas)
          .Include(x => x.Povos)
          .Include(x => x.Mundo)
          .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Conto?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Mundo)
            .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<Conto?> UpdateAsync(Conto conto)
    {
        var existingConto = await tavernaDbContext.Contos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Mundo)
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

            return existingConto;
        }

        return null;
    }
}

