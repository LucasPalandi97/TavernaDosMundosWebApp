using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IContinenteRepository
{
    Task<IEnumerable<Continente>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Continente>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize);
    Task<Continente?> GetAsync(Guid id, int page, int pageSize);
    Task<Continente?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize);
    Task<Continente> AddAsync(Continente continente);
    Task<Continente?> UpdateAsync(Continente continente, int page, int pageSize);
    Task<Continente?> DeleteAsync(Guid id);
}
