using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;


public class Conto
{
   public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Corpo { get; set; }
    public Autor Autor { get; set; }
    public bool AudioDrama { get; set; }
    public string? ImgCard { get; set; }
    public string? ImgBox { get; set; } 
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
    public bool Visible { get; set; }
    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }


    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
   

}