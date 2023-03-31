using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Web.Models.Domain.Enums;

namespace TdM.Web.Models.Domain;


public class Conto
{

    public Guid ContoId { get; set; }
    [MaxLength(32)]
    [Required]
    [Display(Name = "Título")]
    public string Titulo { get; set; }
    [Required]
    public string Corpo { get; set; }
    [Display(Name = "Autor")]
    public Autor Autor { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Data { get; set; }
    [Required]
    [Display(Name = "Audio Drama")]
    public AudioDrama AudioDrama { get; set; }
    [ForeignKey("MundoId")]
    public virtual Mundo? Mundo { get; set; }
    [ForeignKey("ContinenteId")]
    public virtual Continente? Continente { get; set; }
    [ForeignKey("RegiaoId")]
    public virtual Regiao? Regiao { get; set; }
    [ForeignKey("PovoId")]
    public virtual Povo? Povo { get; set; }
    [ForeignKey("CriaturaId")]
    public virtual Criatura? Criatura { get; set; }
    [ForeignKey("PersonagemId")]
    public virtual Personagem? Personagem { get; set; }
}