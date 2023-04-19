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

    public async Task<IEnumerable<Criatura>> GetAllAsync()
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Criaturas
            .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .ToListAsync();
    }

    public async Task<IEnumerable<Criatura>> GetAllByMundoAsync(Guid mundoId)
    {
        return await tavernaDbContext.Criaturas
           .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .Where(x => x.Mundo.Id == mundoId)
           .ToListAsync();
    }

    public async Task<Criatura?> GetAsync(Guid id)
    {
        return await tavernaDbContext.Criaturas
            .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Criatura>> GetAllByRegiao(object selectedRegiaoIds)
    {
        if (selectedRegiaoIds is Guid)
        {
            return await tavernaDbContext.Criaturas
            .Where(p => p.Regioes
            .Any(pp => pp.Id == (Guid)selectedRegiaoIds))
            .ToListAsync();
        }
        else if (selectedRegiaoIds is List<Guid> selectedRegiaoIdsList)
        {
            return await tavernaDbContext.Criaturas
            .Where(p => p.Regioes
            .Any(pp => selectedRegiaoIdsList.Contains(pp.Id)))
            .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Criatura?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Criaturas
            .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<Criatura?> UpdateAsync(Criatura criatura)
    {
        var existingCriatura = await tavernaDbContext.Criaturas
            .Include(x => x.Continentes)
           .Include(x => x.Regioes)
           .Include(x => x.Povos)
           .Include(x => x.Contos)
           .Include(x => x.Mundo)
           .FirstOrDefaultAsync(x => x.Id == criatura.Id);

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

            await tavernaDbContext.SaveChangesAsync();

            return existingCriatura;
        }

        return null;
    }
}
