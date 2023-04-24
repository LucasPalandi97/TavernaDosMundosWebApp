
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;

public class Personagem
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Titulo { get; set; }
    public Classe? Classe { get; set; }
    public Raca? Raca { get; set; }
    public string CurtaDescricao { get; set; }
    public string Biografia { get; set; }
    public string? ImgCard { get; set; }
    public string? ImgBox { get; set; }
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
    public bool Visible { get; set; }
    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }
    [ForeignKey("ContinenteFK")]
    public virtual Continente? Continente { get; set; }
    [ForeignKey("RegiaoFK")]
    public virtual Regiao? Regiao { get; set; }


    public ICollection<Conto>? Contos { get; set; }
    public ICollection<Povo>? Povos { get; set; }

}
