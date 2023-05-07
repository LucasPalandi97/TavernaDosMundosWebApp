using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IRegiaoRepository
{
    Task<IEnumerable<Regiao>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Regiao>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize);
    Task<IEnumerable<Regiao>>? GetAllByContinenteAsync(object selectedContinentIds, int page, int pageSize);
    Task<Regiao?> GetAsync(Guid id, int page, int pageSize);
    Task<Regiao?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize);
    Task<Regiao> AddAsync(Regiao regiao);
    Task<Regiao?> UpdateAsync(Regiao regiao, int page, int pageSize);
    Task<Regiao?> DeleteAsync(Guid id);
}
