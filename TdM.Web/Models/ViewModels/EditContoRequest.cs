using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Web.Models.ViewModels;

public class EditContoRequest
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Corpo { get; set; }
    public Autor Autor { get; set; }
    public bool AudioDrama { get; set; }
    public string? ImgCard { get; set; }
    public string? ImgBox { get; set; }
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
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
