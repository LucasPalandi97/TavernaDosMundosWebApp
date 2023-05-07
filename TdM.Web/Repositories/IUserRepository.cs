using Microsoft.AspNetCore.Identity;

namespace TdM.Web.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAllAsync(int page, int pageSize);
}
