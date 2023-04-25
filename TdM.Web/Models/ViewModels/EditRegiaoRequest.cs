using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain;

namespace TdM.Web.Models.ViewModels;

public class EditRegiaoRequest
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(32, ErrorMessage = "Region length can't be more than 50 Characters.")]
    [Display(Name = "Region")]
    public string Nome { get; set; }

    [Required]
    [StringLength(350, ErrorMessage = "Short Description length can't be more than 350 Characters.")]
    [Display(Name = "Short Description")]
    public string CurtaDescricao { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Descricao { get; set; }

    public string? Simbolo { get; set; }

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
    public string? SelectedContinente { get; set; }
}
