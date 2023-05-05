using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain.Enums;


namespace TdM.Web.Models.ViewModels;

public class AddContoRequest
{
    [Required]
    [StringLength(50, ErrorMessage = "Title length can't be more than 50 characters.")]
    [Display(Name = "Title")]
    public string Titulo { get; set; }

    [Required]
    [Display(Name = "Body text")]
    public string Corpo { get; set; }

    [Required]
    [Display(Name = "Author")]
    public Autor Autor { get; set; }

    [Required]
    [Display(Name = "Audio Drama")]
    public bool AudioDrama { get; set; }

    public string? ImgCard { get; set; }

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


    public IEnumerable<SelectListItem>? Mundos { get; set; }
    public string? SelectedMundo { get; set; }
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

}
