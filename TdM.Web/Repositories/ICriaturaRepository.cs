using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface ICriaturaRepository
{
    Task<IEnumerable<Criatura>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Criatura>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize);
    Task<IEnumerable<Criatura>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize);
    Task<Criatura?> GetAsync(Guid id, int page, int pageSize);
    Task<Criatura?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize); 
    Task<bool> UrlHandleExists(string urlHandle);
    Task<Criatura> AddAsync(Criatura criatura);
    Task<Criatura?> UpdateAsync(Criatura criatura, int page, int pageSize);
    Task<Criatura?> DeleteAsync(Guid id);
}
