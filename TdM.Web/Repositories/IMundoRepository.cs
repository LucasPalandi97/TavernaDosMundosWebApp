using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IMundoRepository
{
    Task<IEnumerable<Mundo>> GetAllAsync();

    Task<Mundo?> GetAsync(Guid id);
    //Task<Mundo?> GetByUrlHandleAsync(string urlHandle);

    Task<Mundo> AddAsync(Mundo mundo);

    Task<Mundo?> UpdateAsync(Mundo mundo);

    Task<Mundo?> DeleteAsync(Guid id);
}
