namespace TdM.Database.Models.Domain;
using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain.Enums;

public class Mundo
{

    public Guid Id { get; set; }

    [MaxLength(32)]
    [Required]
    [Display(Name = "Mundo")]
    public string Nome { get; set; }

    [Display(Name = "Descrição Prévia")]
    public string CurtaDescricao { get; set; }

    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    [Display(Name = "Autor")]
    public Autor Autor { get; set; }

    public string? ImgBox { get; set; }

    public DateTime PublishedDate { get; set; }

    public string UrlHandle { get; set; }

    public bool Visible { get; set; }
 

    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
    public ICollection<Conto>? Contos { get; set; }

   
}


