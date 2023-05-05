using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IMundoRepository
{
    Task<IEnumerable<Mundo>> GetAllAsync(int page, int pageSize);

    Task<Mundo?> GetAsync(Guid id, int page, int pageSize);

    Task<Mundo?> GetByUrlHandleAsync(string urlHandle, int page, int pageSize);

    Task<Mundo> AddAsync(Mundo mundo);

    Task<Mundo?> UpdateAsync(Mundo mundo, int page, int pageSize);

    Task<Mundo?> DeleteAsync(Guid id);
}
