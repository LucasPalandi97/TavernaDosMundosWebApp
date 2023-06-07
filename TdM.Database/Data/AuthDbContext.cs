using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TdM.Database.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminRoleId = "728b2541-6b59-4e4b-8903-932da4896354";
        var superAdminRoleId = "4971067b-f956-43c4-bf06-9ddbf7f0840e";
        var userRoleId = "52d3b884-61d9-4cca-8430-9755b38aab9a";

        // Seed Roles (User, Admin, SuperAdmin)
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);

        // Seed SuperAdminUser
        var superAdminId = "FE39CB62-A779-41D5-A2A7-F35FB81D6719";
        var superAdminUser = new IdentityUser
        {
            UserName = "Palandi",
            Email = "lucaspalandi97@gmail.com",
            NormalizedUserName = "Palandi".ToUpper(),
            NormalizedEmail = "lucaspalandi97@gmail.com".ToUpper(),
            Id = superAdminId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "PastelDosMundos@2023");

        builder.Entity<IdentityUser>().HasData(superAdminUser);

        // Add All roles to SuperAdminUser
        var superAdminRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = superAdminId
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}
