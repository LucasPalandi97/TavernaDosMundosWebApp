using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Web.Models.Domain.Enums;

namespace TdM.Web.Models.Domain;

public class Povo
{
    public Guid PovoId { get; set; }
    [MaxLength(32)]
    [Required]
    [Display(Name = "Povo")]
    public string Nome { get; set; }
    [Display(Name = "Raça")]
    public Raca? ClassRaca { get; set; }
    [Required]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    [ForeignKey("MundoId")]
    public Mundo? Mundo { get; set; }
    public ICollection<Conto>? Contos { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
}
