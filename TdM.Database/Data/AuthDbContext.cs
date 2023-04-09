using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TdM.Database.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
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
        var superAdminId = "467c51e6-2773-4676-be93-ccc8f7b64de9";
        var superAdminUser = new IdentityUser
        {
            UserName = "superadmin@taverna.com",
            Email = "superadmin@taverna.com",
            NormalizedUserName = "superadmin@taverna.com".ToUpper(),
            NormalizedEmail = "superadmin@taverna.com".ToUpper(),
            Id = superAdminId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "pasteldosmundos");

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
