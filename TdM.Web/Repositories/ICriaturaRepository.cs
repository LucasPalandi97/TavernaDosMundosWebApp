using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface ICriaturaRepository
{
    Task<IEnumerable<Criatura>> GetAllAsync();

    Task<Criatura?> GetAsync(Guid id);

    Task<Criatura?> GetByUrlHandleAsync(string urlHandle);
    Task<Criatura> AddAsync(Criatura mundo);

    Task<Criatura?> UpdateAsync(Criatura mundo);

    Task<Criatura?> DeleteAsync(Guid id);
}
