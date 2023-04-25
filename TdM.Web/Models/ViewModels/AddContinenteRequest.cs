using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TdM.Web.Models.ViewModels;

public class AddContinenteRequest
{
    [Required]
    [StringLength(32, ErrorMessage = "Continent length can't be more than 50 Characters.")]
    [Display(Name = "Continent")]
    public string Nome { get; set; }

    [Required]
    [StringLength(350, ErrorMessage = "Short Description length can't be more than 350 Characters.")]
    [Display(Name = "Short Description")]
    public string CurtaDescricao { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Descricao { get; set; }

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
    public IEnumerable<SelectListItem>? Regioes { get; set; }
    public string[]? SelectedRegioes { get; set; } = Array.Empty<string>();
}
