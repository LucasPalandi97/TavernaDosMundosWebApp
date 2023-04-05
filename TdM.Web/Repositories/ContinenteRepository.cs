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
        return await tavernaDbContext.Continentes.ToListAsync();
    }

    public Task<Continente?> GetAsync(Guid id)
    {
        return tavernaDbContext.Continentes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Continente?> UpdateAsync(Continente continente)
    {

        var existingContinente = await tavernaDbContext.Continentes.FindAsync(continente.Id);

        if (existingContinente != null)
        {
            existingContinente.Nome = continente.Nome;
            existingContinente.Descricao = continente.Descricao;
            existingContinente.ImgCard = continente.ImgCard;
            existingContinente.ImgBox = continente.ImgBox;
            existingContinente.Visible = continente.Visible;
            await tavernaDbContext.SaveChangesAsync();

            return existingContinente;
        }

        return null;
    }

  
}
