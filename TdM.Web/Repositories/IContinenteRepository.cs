using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public interface IContinenteRepository
{
    Task<IEnumerable<Continente>> GetAllAsync();

    Task<Continente?> GetAsync(Guid id);

    Task<Continente> AddAsync(Continente continente);

    Task<Continente?> UpdateAsync(Continente continente);

    Task<Continente?> DeleteAsync(Guid id);

}
