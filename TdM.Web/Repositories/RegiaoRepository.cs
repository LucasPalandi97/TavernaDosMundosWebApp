using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class RegiaoRepository : IRegiaoRepository
{
    private readonly TavernaDbContext tavernaDbContext;

    public RegiaoRepository(TavernaDbContext tavernaDbContext)
    {
        this.tavernaDbContext = tavernaDbContext;
    }
    public async Task<Regiao> AddAsync(Regiao regiao)
    {
        await tavernaDbContext.AddAsync(regiao);
        await tavernaDbContext.SaveChangesAsync();
        return regiao;
    }

    public async Task<Regiao?> DeleteAsync(Guid id)
    {

        var existingRegiao = await tavernaDbContext.Regioes.FindAsync(id);

        if (existingRegiao != null)
        {
            tavernaDbContext.Regioes.Remove(existingRegiao);
            await tavernaDbContext.SaveChangesAsync();
            return existingRegiao;
        }
        return null;

    }

    public async Task<IEnumerable<Regiao>> GetAllAsync()
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo).ToListAsync();
    }

    public async Task<IEnumerable<Regiao>> GetAllByMundoAsync(Guid id)
    {
        //return list and include navigation Icollection from model database
        return await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo).ToListAsync();
    }

    public async Task<Regiao?> GetAsync(Guid id)
    {
        return await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Regiao>>? GetRegioesByContinenteAsync(object selectedContinenteIds)
    {
        if (selectedContinenteIds is Guid)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await tavernaDbContext.Regioes.Where(r => r.Continente.Id == (Guid)selectedContinenteIds).ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        else if (selectedContinenteIds is List<Guid> selectedContinenteIdsList)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await tavernaDbContext.Regioes.Where(r => selectedContinenteIdsList.Contains(r.Continente.Id)).ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Regiao?> GetByUrlHandleAsync(string urlHandle)
    {
        return await tavernaDbContext.Regioes.Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<Regiao?> UpdateAsync(Regiao regiao)
    {
        var existingRegiao = await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo).FirstOrDefaultAsync(x => x.Id == regiao.Id);

        if (existingRegiao != null)
        {
            existingRegiao.Nome = regiao.Nome;
            existingRegiao.CurtaDescricao = regiao.CurtaDescricao;
            existingRegiao.Descricao = regiao.Descricao;
            existingRegiao.Simbolo = regiao.Simbolo;
            existingRegiao.ImgCard = regiao.ImgCard;
            existingRegiao.ImgBox = regiao.ImgBox;
            existingRegiao.PublishedDate = regiao.PublishedDate;
            existingRegiao.UrlHandle = regiao.UrlHandle;
            existingRegiao.Visible = regiao.Visible;
            existingRegiao.Continente = regiao.Continente;
            existingRegiao.Mundo = regiao.Continente?.Mundo;
            await tavernaDbContext.SaveChangesAsync();

            return existingRegiao;
        }

        return null;
    }

}
