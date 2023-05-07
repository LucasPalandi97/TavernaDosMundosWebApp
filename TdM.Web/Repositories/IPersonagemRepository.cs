using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IPersonagemRepository
{
    Task<IEnumerable<Personagem>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<Personagem>> GetAllByMundoAsync(Guid mundoId, int page, int pageSize);
    Task<IEnumerable<Personagem>>? GetAllByRegiaoAsync(object selectedContinentIds, int page, int pageSize);
    Task<Personagem?> GetAsync(Guid id, int page, int pageSize);
    Task<Personagem?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize);
    Task<Personagem> AddAsync(Personagem personagem);
    Task<Personagem?> UpdateAsync(Personagem personagem, int page, int pageSize);
    Task<Personagem?> DeleteAsync(Guid id);
}
