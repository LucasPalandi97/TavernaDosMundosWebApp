using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TdM.Database.Models.Domain;

public class Regiao
{

    public Guid Id { get; set; }

    [Required]
    [MaxLength(32)]
    [Display(Name = "Região")]
    public string Nome { get; set; }

    [Required]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    [Display(Name = "Descrição Prévia")]
    public string CurtaDescricao { get; set; }

    [Display(Name = "Símbolo")]
    public string? Simbolo { get; set; }

    public string? ImgCard { get; set; }

    public string? ImgBox { get; set; }

    public DateTime PublishedDate { get; set; }

    public string UrlHandle { get; set; }

    [Required]
    public bool Visible { get; set; }

    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }

    [ForeignKey("ContinenteFK")]
    public Continente? Continente { get; set; }


    public ICollection<Conto>? Contos { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Povo>? Povos { get; set; }


}
