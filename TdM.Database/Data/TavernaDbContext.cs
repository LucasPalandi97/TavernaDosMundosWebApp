using Microsoft.EntityFrameworkCore;
using TdM.Database.Models.Domain;
namespace TdM.Database.Data;


public class TavernaDbContext : DbContext
{

    public TavernaDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Mundo> Mundos { get; set; }
    public DbSet<Continente> Continentes { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Personagem> Personagens { get; set; }
    public DbSet<Povo> Povos { get; set; }
    public DbSet<Criatura> Criaturas { get; set; }
    public DbSet<Conto> Contos { get; set; }



}
