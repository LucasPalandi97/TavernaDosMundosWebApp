using Microsoft.AspNetCore.Mvc.Rendering;
using TdM.Database.Models.Domain;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Web.Models.ViewModels;

public class EditPersonagemRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Titulo { get; set; }
    public Classe? Classe { get; set; }
    public Raca? Raca { get; set; }
    public string CurtaDescricao { get; set; }
    public string Biografia { get; set; }
    public string? ImgCard { get; set; }
    public string? ImgBox { get; set; }
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
    public bool Visible { get; set; }

    public IEnumerable<SelectListItem>? Mundos { get; set; }
    public string? SelectedMundo { get; set; }

    public IEnumerable<SelectListItem>? Continentes { get; set; }
    public string? SelectedContinente { get; set; }

    public IEnumerable<SelectListItem>? Regioes { get; set; }
    public string? SelectedRegiao { get; set; }
}
