using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IContoRepository
{
    Task<IEnumerable<Conto>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Conto>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize);
    Task<IEnumerable<Conto>> GetAllByContinenteAsync(object selectedContinenteIds, int page, int pageSize);
    Task<IEnumerable<Conto>> GetAllByRegiaoAsync(object selectedRegiaoIds, int page, int pageSize);
    Task<IEnumerable<Conto>> GetAllByPersonagemAsync(object selectedPersonagemIds, int page, int pageSize);
    Task<IEnumerable<Conto>> GetAllByCriaturaAsync(object selectedCriaturaIds, int page, int pageSize);
    Task<IEnumerable<Conto>> GetAllByPovoAsync(object selectedPovoIds, int page, int pageSize);
    Task<Conto?> GetAsync(Guid id, int page, int pageSize);
    Task<Conto?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize);
    Task<Conto> AddAsync(Conto conto);
    Task<Conto?> UpdateAsync(Conto conto, int page, int pageSize);
    Task<Conto?> DeleteAsync(Guid id);
}
