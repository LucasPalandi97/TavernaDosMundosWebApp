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

    public async Task<IEnumerable<Continente>> GetAllAsync()
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Continentes.Include(x => x.Mundo).ToListAsync();
    }

    public Task<Continente?> GetAsync(Guid id)
    {
        return tavernaDbContext.Continentes.Include(x => x.Mundo).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Continente?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Continentes.Include(x => x.Mundo).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<Continente?> UpdateAsync(Continente continente)
    {

        var existingContinente = await tavernaDbContext.Continentes.Include(x => x.Mundo).FirstOrDefaultAsync(x => x.Id == continente.Id);

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
            await tavernaDbContext.SaveChangesAsync();

            return existingContinente;
        }

        return null;
    }

  
}
