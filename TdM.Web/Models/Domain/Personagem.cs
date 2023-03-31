
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Web.Models.Domain.Enums;

namespace TdM.Web.Models.Domain;

public class Personagem
{

    public Guid PersonagemId { get; set; }
    [MaxLength(32)]
    [Required]
    public string Nome { get; set; }
    [MaxLength(32)]
    [Display(Name = "Título")]
    public string Titulo { get; set; }
    [Display(Name = "Classe")]
    public Classe? ClassClasse { get; set; }
    [Display(Name = "Raça")]
    public Raca? ClassRaca { get; set; }
    [Required]
    [Display(Name = "Biografia")]
    public string Biografia { get; set; }
    [ForeignKey("MundoId")]
    public virtual Mundo? Mundo { get; set; }
    [ForeignKey("RegiaoId")]
    public virtual Regiao? Regiao { get; set; }
    [ForeignKey("PovoId")]
    public virtual Povo? Povo { get; set; }
    public ICollection<Conto>? Contos { get; set; }



}
