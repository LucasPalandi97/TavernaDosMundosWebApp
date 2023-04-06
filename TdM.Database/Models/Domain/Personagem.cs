
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;

public class Personagem
{

    public Guid Id { get; set; }

    [MaxLength(32)]
    [Required]
    public string Nome { get; set; }

    [MaxLength(32)]
    [Display(Name = "Título")]
    public string Titulo { get; set; }

    [Display(Name = "Classe")]
    public Classe? Classe { get; set; }

    [Display(Name = "Raça")]
    public Raca? Raca { get; set; }

    [Required]
    [Display(Name = "Biografia")]
    public string Biografia { get; set; }

    public string? ImgCard { get; set; }

    public string? ImgBox { get; set; }

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
