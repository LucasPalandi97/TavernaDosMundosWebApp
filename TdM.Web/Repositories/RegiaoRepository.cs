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

    public async Task<IEnumerable<Regiao>> GetAllAsync(int page, int pageSize)
    {
        return await tavernaDbContext.Regioes
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Mundo).ToListAsync();
    }

    public async Task<IEnumerable<Regiao>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Regioes
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Regiao>> GetAllWithoutContinenteAsync(Guid mundoId, int page, int pageSize)
    {
        return await tavernaDbContext.Regioes
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .Where(x => x.Mundo.Id == mundoId && x.Continente == null)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


    public async Task<Regiao?> GetAsync(Guid id, int page, int pageSize)
    {
        return await tavernaDbContext.Regioes
            .Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .Where(x => x.Id == id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Regiao>>? GetAllByContinenteAsync(object selectedContinenteIds, int page, int pageSize)
    {
        if (selectedContinenteIds is Guid)
        {
            return await tavernaDbContext.Regioes
                .Where(r => r.Continente.Id == (Guid)selectedContinenteIds)
                .ToListAsync();
        }
        else if (selectedContinenteIds is List<Guid> selectedContinenteIdsList)
        {
            return await tavernaDbContext.Regioes
                .Where(r => selectedContinenteIdsList
                .Contains(r.Continente.Id))
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException("Invalid argument type");
        }
    }

    public async Task<Regiao?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize)
    {
        return await tavernaDbContext.Regioes.Include(x => x.Personagens)
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .Where(x => x.UrlHandle == urlHandle)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();
    }

    public async Task<Regiao?> UpdateAsync(Regiao regiao, int page, int pageSize)
    {
        var existingRegiao = await tavernaDbContext.Regioes
            .Include(x => x.Criaturas)
            .Include(x => x.Povos)
            .Include(x => x.Contos)
            .Include(x => x.Continente)
            .Include(x => x.Mundo)
            .Where(x => x.Id == regiao.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();

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
            existingRegiao.Mundo = regiao.Mundo;
            existingRegiao.Continente = regiao.Continente;
            existingRegiao.Personagens = regiao.Personagens;
            existingRegiao.Criaturas = regiao.Criaturas;
            existingRegiao.Povos = regiao.Povos;
            existingRegiao.Contos = regiao.Contos;
            await tavernaDbContext.SaveChangesAsync();

            return existingRegiao;
        }
        return null;
    }


}
