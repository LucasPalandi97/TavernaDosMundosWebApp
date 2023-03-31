using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TdM.Web.Models.Domain;

public class Continente
{

    public Guid ContinenteId { get; set; }
    [MaxLength(32)]
    [Required]
    [Display(Name = "Continente")]
    public string Nome { get; set; }
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    [ForeignKey("MundoId")]
    public virtual Mundo? Mundo { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Conto>? Contos { get; set; }
}


