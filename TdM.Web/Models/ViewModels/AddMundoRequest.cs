using Microsoft.AspNetCore.Mvc.Rendering;

namespace TdM.Web.Models.ViewModels
{
    public class AddMundoRequest
    {
        public string Nome { get; set; }
        public string CurtaDescricao { get; set; }
        public string Descricao { get; set; }
        public int Autor { get; set; }
        public string? ImgBox { get; set; }
        public DateTime PublishedDate { get; set; }
        public string UrlHandle { get; set; }
        public bool Visible { get; set; }


        // Display Continentes 
        public IEnumerable<SelectListItem> Continentes { get; set; }

        //Collect Continente     
        //public string SelectedMundo { get; set; }

         //Collect Multiple itens
         public string?[] SelectedContinentes { get; set; } = Array.Empty<string>();
    }
}
