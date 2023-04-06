﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Database.Models.Domain;

public class Povo
{
    public Guid Id { get; set; }

    [MaxLength(32)]
    [Required]
    [Display(Name = "Povo")]
    public string Nome { get; set; }

    [Display(Name = "Raça")]
    public Raca? Raca { get; set; }

    [Required]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    public string? ImgCard { get; set; }

    public string? ImgBox { get; set; }

    [Required]
    public bool Visible { get; set; }

    [ForeignKey("MundoFK")]
    public virtual Mundo? Mundo { get; set; }


    public ICollection<Conto>? Contos { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
}
