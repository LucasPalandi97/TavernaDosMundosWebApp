using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class CriaturaRepository : ICriaturaRepository
{
    private readonly TavernaDbContext tavernaDbContext;

    public CriaturaRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;
    }

    public async Task<Criatura> AddAsync(Criatura criatura)
    {
        await tavernaDbContext.AddAsync(criatura);
        await tavernaDbContext.SaveChangesAsync();

        return criatura;
    }

    public async Task<Criatura?> DeleteAsync(Guid id)
    {
        var existingCriatura = await tavernaDbContext.Criaturas.FindAsync(id);

        if (existingCriatura != null)
        {
            tavernaDbContext.Criaturas.Remove(existingCriatura);
            await tavernaDbContext.SaveChangesAsync();

            return existingCriatura;
        }
        return null;
    }

    public async Task<IEnumerable<Criatura>> GetAllAsync(int page, int pageSize)
    {
        return await tavernaDbContext.Criaturas
               .Include(x => x.Mundo)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
    }

    public async Task<IEnumerable<Criatura>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Criaturas
               .Include(x => x.Mundo)
               .Where(x => x.Mundo.Id == mundoId)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
    }

    public async Task<Criatura?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Criaturas
               .Include(x => x.Continentes)
               .Include(x => x.Regioes)
               .Include(x => x.Povos)
               .Include(x => x.Contos)
               .Include(x => x.Mundo)
               .Where(x => x.Id == id)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Criatura>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Criaturas
                .Where(p => p.Regioes
                .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
        {
            return await tavernaDbContext.Criaturas
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

    public async Task<Criatura?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Criaturas
               .Include(x => x.Continentes)
               .Include(x => x.Regioes)
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
        bool urlHandleExists = await tavernaDbContext.Criaturas.AnyAsync(m => m.UrlHandle == urlHandle);
        return urlHandleExists;
    }
    public async Task<Criatura?> UpdateAsync(Criatura criatura, int page, int pageSize)
    {
        var existingCriatura = await tavernaDbContext.Criaturas
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .Where(x => x.Id == criatura.Id)
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .FirstOrDefaultAsync();

        if (existingCriatura != null)
        {
            existingCriatura.Nome = criatura.Nome;
            existingCriatura.Tipo = criatura.Tipo;
            existingCriatura.CurtaDescricao = criatura.CurtaDescricao;
            existingCriatura.Descricao = criatura.Descricao;
            existingCriatura.ImgCard = criatura.ImgCard;
            existingCriatura.ImgBox = criatura.ImgBox;
            existingCriatura.PublishedDate = criatura.PublishedDate;
            existingCriatura.UrlHandle = criatura.UrlHandle;
            existingCriatura.Visible = criatura.Visible;
            existingCriatura.Mundo = criatura.Mundo;
            existingCriatura.Continentes = criatura.Continentes;
            existingCriatura.Regioes = criatura.Regioes;
            existingCriatura.Povos = criatura.Povos;
            existingCriatura.Contos = criatura.Contos;
            await tavernaDbContext.SaveChangesAsync();

            return existingCriatura;

        }
        return null;
    }
}
