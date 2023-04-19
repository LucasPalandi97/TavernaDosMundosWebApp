using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;

namespace TdM.Web.Models.ViewModels;

public class EditPovoRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CurtaDescricao { get; set; }
    public string Descricao { get; set; }
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
    public IEnumerable<SelectListItem>? Contos { get; set; }
    public string[]? SelectedContos { get; set; } = Array.Empty<string>();
}
