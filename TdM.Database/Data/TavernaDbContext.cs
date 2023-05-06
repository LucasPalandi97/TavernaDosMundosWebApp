using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TdM.Database.Models.Domain;

namespace TdM.Database.Data;

public class TavernaDbContext : DbContext
{    
    public DbSet<Mundo> Mundos { get; set; }
    public DbSet<Continente> Continentes { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Personagem> Personagens { get; set; }
    public DbSet<Povo> Povos { get; set; }
    public DbSet<Criatura> Criaturas { get; set; }
    public DbSet<Conto> Contos { get; set; }

    private readonly IMemoryCache _cache;

    public TavernaDbContext(DbContextOptions<TavernaDbContext> options, IMemoryCache cache) : base(options)
    {
        _cache = cache;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mundo>()
                    .HasMany(c => c.Continentes)
                    .WithOne(m => m.Mundo)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Mundo>()
                    .HasMany(r => r.Regioes)
                    .WithOne(m => m.Mundo)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Mundo>()
                    .HasMany(pe => pe.Personagens)
                    .WithOne(m => m.Mundo)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Mundo>()
                    .HasMany(po => po.Povos)
                    .WithOne(m => m.Mundo)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Mundo>()
                    .HasMany(cr => cr.Criaturas)
                    .WithOne(m => m.Mundo)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Mundo>()
                    .HasMany(co => co.Contos)
                    .WithOne(m => m.Mundo)
                    .OnDelete(DeleteBehavior.SetNull);


        modelBuilder.Entity<Continente>()
                    .HasMany(r => r.Regioes)
                    .WithOne(c => c.Continente)
                    .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Continente>()
                    .HasMany(pe => pe.Personagens)
                    .WithOne(c => c.Continente)
                    .OnDelete(DeleteBehavior.SetNull);


        modelBuilder.Entity<Regiao>()
                    .HasMany(pe => pe.Personagens)
                    .WithOne(r => r.Regiao)
                    .OnDelete(DeleteBehavior.SetNull);

     
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMemoryCache(_cache);

        optionsBuilder.EnableSensitiveDataLogging();
    }

}
