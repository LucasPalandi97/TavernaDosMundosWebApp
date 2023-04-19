using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IPersonagemRepository
{
    Task<IEnumerable<Personagem>> GetAllAsync();
    Task<IEnumerable<Personagem>> GetAllByMundoAsync(Guid mundoId);
    Task<Personagem?> GetAsync(Guid id);
    Task<IEnumerable<Personagem>>? GetPersonagensByRegiaoAsync(object selectedContinentIds);
    Task<Personagem?> GetByUrlHandleAsync(string urlHandle);
    Task<Personagem> AddAsync(Personagem personagem);
    Task<Personagem?> UpdateAsync(Personagem personagem);
    Task<Personagem?> DeleteAsync(Guid id);
}
