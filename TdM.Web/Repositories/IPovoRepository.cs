using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IPovoRepository
{
    Task<IEnumerable<Povo>> GetAllAsync();
    Task<IEnumerable<Povo>> GetAllByMundoAsync(Guid mundoId);
    Task<IEnumerable<Povo>> GetAllByPersonagem(object selectedPersonagemIds);
    Task<IEnumerable<Povo>> GetAllByRegiao(object selectedRegiaoIds);
    Task<Povo?> GetAsync(Guid id);
    Task<Povo?> GetByUrlHandleAsync(string urlHandle);
    Task<Povo> AddAsync(Povo povo);
    Task<Povo?> UpdateAsync(Povo povo);
    Task<Povo?> DeleteAsync(Guid id);
}
