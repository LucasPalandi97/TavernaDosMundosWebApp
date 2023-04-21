using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface ICriaturaRepository
{
    Task<IEnumerable<Criatura>> GetAllAsync();
    Task<IEnumerable<Criatura>> GetAllByMundoAsync(Guid mundoId);
    Task<IEnumerable<Criatura>> GetAllByRegiao(object selectedRegiaoIds);
    Task<Criatura?> GetAsync(Guid id);
    Task<Criatura?> GetByUrlHandleAsync(string urlHandle);
    Task<Criatura> AddAsync(Criatura criatura);
    Task<Criatura?> UpdateAsync(Criatura criatura);
    Task<Criatura?> DeleteAsync(Guid id);
}
