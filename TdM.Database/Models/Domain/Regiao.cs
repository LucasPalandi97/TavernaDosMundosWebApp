using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TdM.Database.Models.Domain;

public class Regiao
{

    public Guid RegiaoId { get; set; }
    [Required]
    [MaxLength(32)]
    [Display(Name = "Região")]
    public string Nome { get; set; }
    [MaxLength(3)]
    [Display(Name = "Símbolo")]
    public char Simbolo { get; set; }
    [Required]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    [ForeignKey("ContinenteId")]
    public Continente? Continente { get; set; }
    [ForeignKey("MundoId")]
    public Mundo? Mundo { get; set; }


    public ICollection<Conto>? Contos { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Povo>? Povos { get; set; }


}
