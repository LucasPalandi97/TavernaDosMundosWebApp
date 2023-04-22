using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IContoRepository
{
    Task<IEnumerable<Conto>> GetAllAsync();
    Task<IEnumerable<Conto>> GetAllByMundoAsync(Guid mundoId);
    Task<IEnumerable<Conto>> GetAllByContinenteAsync(object selectedContinenteIds);
    Task<IEnumerable<Conto>> GetAllByRegionAsync(object selectedRegiaoIds);
    Task<IEnumerable<Conto>> GetAllByPersonagemAsync(object selectedPersonagemIds);
    Task<IEnumerable<Conto>> GetAllByCriaturaAsync(object selectedCriaturaIds);
    Task<IEnumerable<Conto>> GetAllByPovoAsync(object selectedPovoIds);
    Task<Conto?> GetAsync(Guid id);
    Task<Conto?> GetByUrlHandleAsync(string urlHandle);
    Task<Conto> AddAsync(Conto conto);
    Task<Conto?> UpdateAsync(Conto conto);
    Task<Conto?> DeleteAsync(Guid id);
}
