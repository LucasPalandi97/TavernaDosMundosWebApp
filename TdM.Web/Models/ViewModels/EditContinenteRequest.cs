using Microsoft.AspNetCore.Mvc.Rendering;

namespace TdM.Web.Models.ViewModels;

public class EditContinenteRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string? ImgCard { get; set; }
    public string? ImgBox { get; set; }
    public bool Visible { get; set; }

    // Display Mundos 
    public IEnumerable<SelectListItem> Mundos { get; set; }

    //Collect Continente     
    public string? SelectedMundo { get; set; }

    //Collect Multiple itens
    //public string[] SelectedContinentes { get; set; } = Array.Empty<string>();
}

