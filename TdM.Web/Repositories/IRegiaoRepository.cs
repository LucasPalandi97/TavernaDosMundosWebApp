using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IRegiaoRepository
{
    Task<IEnumerable<Regiao>> GetAllAsync();

    Task<Regiao?> GetAsync(Guid id);

    Task<Regiao?> GetByUrlHandleAsync(string urlHandle);

    Task<Regiao> AddAsync(Regiao regiao);

    Task<Regiao?> UpdateAsync(Regiao regiao);

    Task<Regiao?> DeleteAsync(Guid id);
}
