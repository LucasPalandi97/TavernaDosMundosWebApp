using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels
{
    public class AddContinenteRequest
    {
 
        [MaxLength(32, ErrorMessage = "Name has to be a maximum of 32 characteres")]
        public string Nome { get; set; }   
        public string CurtaDescricao { get; set; }
        public string Descricao { get; set; }      
        public string? ImgCard { get; set; }
        public string? ImgBox { get; set; }
        public DateTime PublishedDate { get; set; }
        public string UrlHandle { get; set; }
        public bool Visible { get; set; }

        // Display Mundos 
        public IEnumerable<SelectListItem> Mundos { get; set; }

        //Collect Continente     
        public string? SelectedMundo { get; set; }

        //Collect Multiple itens
        //public string[] SelectedContinentes { get; set; } = Array.Empty<string>();
    }
}
