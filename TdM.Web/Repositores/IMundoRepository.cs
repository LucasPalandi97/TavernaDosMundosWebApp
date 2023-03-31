﻿using TdM.Web.Models.Domain;

namespace TdM.Web.Repositores;

public interface IMundoRepository
{
    Task<IEnumerable<Mundo>> GetAllAsync();

    Task<Mundo?> GetAsync(Guid id);

    Task<Mundo> AddAsync(Mundo mundo);

    Task<Mundo?> UpdateAsync(Mundo mundo);

    Task<Mundo?> DeleteAsync(Guid id);
}
