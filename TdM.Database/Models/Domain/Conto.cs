using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;


public class Conto
{

    public Guid Id { get; set; }

    [MaxLength(32)]
    [Required]
    [Display(Name = "Título")]
    public string Titulo { get; set; }

    [Required]
    public string Corpo { get; set; }

    [Display(Name = "Autor")]
    public Autor Autor { get; set; }

    [Required]
    [Display(Name = "Audio Drama")]
    public bool AudioDrama { get; set; }

    public string? ImgCard { get; set; }

    public string? ImgBox { get; set; }
  
    public DateTime PublishedDate { get; set; }

    public string UrlHandle { get; set; }
    [Required]
    public bool Visible { get; set; }

    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }


    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
   

}