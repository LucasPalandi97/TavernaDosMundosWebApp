using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext authDbContext;
    private readonly IMemoryCache cache;

    public UserRepository(AuthDbContext authDbContext, IMemoryCache cache)
    {
        this.authDbContext = authDbContext;
        this.cache = cache;
    }

    public async Task<IEnumerable<IdentityUser>> GetAll(int page, int pageSize)
    {
        string cacheKey = $"UserRepository.GetAllAsync_{page}_{pageSize}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<IdentityUser>? result))
        {
            var users = await authDbContext.Users
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var superAdminUser = await authDbContext.Users
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .FirstOrDefaultAsync(x => x.Email == "superadmin@taverna.com");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            result = users;

            if (result != null && result.Any())
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            }
        }
        return result;
    }
}
