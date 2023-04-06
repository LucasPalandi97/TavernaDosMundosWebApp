using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;

public class Criatura
{

    public Guid Id { get; set; }

    [MaxLength(32)]
    [Required]
    [Display(Name = "Criatura")]
    public string Nome { get; set; }

    [Display(Name = "Tipo")]
    public Tipo? Tipo { get; set; }

    [Required]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    public string? ImgCard { get; set; }

    public string? ImgBox { get; set; }

    public bool Visible { get; set; }

    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }


    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Conto>? Contos { get; set; }
}