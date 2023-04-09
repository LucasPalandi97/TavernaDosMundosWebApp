using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain;

namespace TdM.Web.Models.ViewModels;

public class AddRegiaoRequest
{


    [MaxLength(32, ErrorMessage = "Name has to be a maximum of 32 characteres")]
    public string Nome { get; set; }
    public string CurtaDescricao { get; set; }
    public string Descricao { get; set; }
    public string? Simbolo { get; set; }
    public string? ImgCard { get; set; }
    public string? ImgBox { get; set; }
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
    public bool Visible { get; set; }
    public Mundo Mundo { get; set; }

    // Display Continentes 
    public IEnumerable<SelectListItem> Continentes { get; set; }

    //Collect Continente     
    public string? SelectedContinente { get; set; }


    //Collect Multiple itens
    //public string[] SelectedContinentes { get; set; } = Array.Empty<string>();

}
