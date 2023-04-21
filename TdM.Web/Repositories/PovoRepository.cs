using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class PovoRepository : IPovoRepository
{

    private readonly TavernaDbContext tavernaDbContext;

    public PovoRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;
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

    public async Task<IEnumerable<Povo>> GetAllAsync()
    {

        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo).ToListAsync();
    }

    public async Task<IEnumerable<Povo>> GetAllByMundoAsync(Guid mundoId)
    {
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Povo>> GetAllByPersonagem(object selectedPersonagemIds)
    {
        if (selectedPersonagemIds is Guid)
        {
            return await tavernaDbContext.Povos
            .Where(p => p.Personagens
            .Any(pp => pp.Id == (Guid)selectedPersonagemIds))
            .ToListAsync();
        }
        else if (selectedPersonagemIds is List<Guid> selectedPersonagemIdsList)
        {
            return await tavernaDbContext.Povos
            .Where(p => p.Personagens
            .Any(pp => selectedPersonagemIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<IEnumerable<Povo>> GetAllByRegion(object selectedRegiaoIds)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Povos
            .Where(p => p.Regioes
            .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
            .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> sselectedRegiaoIdsList)
        {
            return await tavernaDbContext.Povos
            .Where(p => p.Regioes
            .Any(pp => sselectedRegiaoIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Povo?> GetAsync(Guid id)
    {
        return await tavernaDbContext.Povos
             .Include(x => x.Continentes)
             .Include(x => x.Regioes)
             .Include(x => x.Personagens)
             .Include(x => x.Criaturas)
             .Include(x => x.Contos)
             .Include(x => x.Mundo)
             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Povo?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }


    public async Task<Povo?> UpdateAsync(Povo povo)
    {
       var existingPovo = await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
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
            existingPovo.Continentes= povo.Continentes;
            existingPovo.Regioes= povo.Regioes;
            existingPovo.Personagens= povo.Personagens;
            existingPovo.Criaturas= povo.Criaturas;
            await tavernaDbContext.SaveChangesAsync();

            return existingPovo;
        }

        return null;
    }
}
