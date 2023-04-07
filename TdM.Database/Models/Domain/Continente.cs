using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TdM.Database.Models.Domain;

public class Continente
{

    public Guid Id { get; set; }

    [MaxLength(32)]
    [Required]
    [Display(Name = "Continente")]
    public string Nome { get; set; }

    [Display(Name = "Descrição Prévia")]
    public string CurtaDescricao { get; set; }

    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    public string? ImgCard { get; set; }

    public string? ImgBox { get; set; }
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
    [Required]
    public bool Visible { get; set; }

    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }



    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Conto>? Contos { get; set; }
}


