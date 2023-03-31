using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;

public class Criatura
{

    public Guid CriaturaId { get; set; }
    [MaxLength(32)]
    [Required]
    [Display(Name = "Criatura")]
    public string Nome { get; set; }
    [Display(Name = "Tipo")]
    public Tipo? ClassTipo { get; set; }
    [Required]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    [ForeignKey("MundoId")]
    public virtual Mundo? Mundo { get; set; }

    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Conto>? Contos { get; set; }
}