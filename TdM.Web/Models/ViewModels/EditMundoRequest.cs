using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Web.Models.ViewModels;

public class EditMundoRequest
{
    public Guid Id { get; set; }

    [StringLength(32, ErrorMessage = "World length can't be more than 50 characters.")]
    [Required]
    [Display(Name = "World")]
    public string Nome { get; set; }

    [Required]
    [StringLength(350, ErrorMessage = "Short Description length can't be more than 350 Characters.")]
    [Display(Name = "Short Description")]
    public string CurtaDescricao { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Descricao { get; set; }

    [Required]
    [Display(Name = "Author")]
    public Autor Autor { get; set; }

    public string? ImgBox { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Published Date")]
    public DateTime PublishedDate { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
    [Display(Name = "URL Handle")]
    public string UrlHandle { get; set; }

    [Required]
    public bool Visible { get; set; }


    public IEnumerable<SelectListItem>? Continentes { get; set; }
    public string[]? SelectedContinentes { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Regioes { get; set; }
    public string[]? SelectedRegioes { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Personagens { get; set; }
    public string[]? SelectedPersonagens { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Criaturas { get; set; }
    public string[]? SelectedCriaturas { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Povos { get; set; }
    public string[]? SelectedPovos { get; set; } = Array.Empty<string>();
    public IEnumerable<SelectListItem>? Contos { get; set; }
    public string[]? SelectedContos { get; set; } = Array.Empty<string>();
}
