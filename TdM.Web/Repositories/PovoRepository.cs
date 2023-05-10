using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Povo>> GetAllAsync(int page, int pageSize)
    {
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Povo>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Povo>> GetAllByPersonagemAsync(object selectedPersonagemIds, int page, int pageSize)
    {
        if (selectedPersonagemIds is Guid)
        {
            return await tavernaDbContext.Povos

            .Where(p => p.Personagens
            .Any(pp => pp.Id == (Guid)selectedPersonagemIds))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }
        else if (selectedPersonagemIds is List<Guid> selectedPersonagemIdsList)
        {
            return await tavernaDbContext.Povos

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

    public async Task<IEnumerable<Povo>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Povos
            .Where(p => p.Regioes
            .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
        {
            return await tavernaDbContext.Povos
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
    }

    public async Task<Povo?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.Id == id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<Povo?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Povos
            .Include(x => x.Continentes)
            .Include(x => x.Regioes)
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Contos)
            .Include(x => x.Mundo)
            .Where(x => x.UrlHandle == urlHandle)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<Povo?> UpdateAsync(Povo povo, int page, int pageSize)
    {
        var existingPovo = await tavernaDbContext.Povos
             .Include(x => x.Continentes)
             .Include(x => x.Regioes)
             .Include(x => x.Personagens)
             .Include(x => x.Criaturas)
             .Include(x => x.Contos)
             .Include(x => x.Mundo)
             .Where(x => x.Id == povo.Id)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .FirstOrDefaultAsync();

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
            existingPovo.Continentes = povo.Continentes;
            existingPovo.Regioes = povo.Regioes;
            existingPovo.Personagens = povo.Personagens;
            existingPovo.Criaturas = povo.Criaturas;
            existingPovo.Contos = povo.Contos;

            await tavernaDbContext.SaveChangesAsync();

            return existingPovo;
        }
        return null;
    }
}
