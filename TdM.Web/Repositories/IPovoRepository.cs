using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IPovoRepository
{
    Task<IEnumerable<Povo>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Povo>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize);
    Task<IEnumerable<Povo>> GetAllByPersonagemAsync(object selectedPersonagemIds, int page, int pageSize);
    Task<IEnumerable<Povo>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize);
    Task<Povo?> GetAsync(Guid id, int page, int pageSize);
    Task<Povo?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize);
    Task<Povo> AddAsync(Povo povo);
    Task<Povo?> UpdateAsync(Povo povo, int page, int pageSize);
    Task<Povo?> DeleteAsync(Guid id);
}
