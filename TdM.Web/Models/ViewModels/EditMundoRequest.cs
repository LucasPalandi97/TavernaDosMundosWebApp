using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Web.Models.ViewModels
{
    public class EditMundoRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Autor Autor { get; set; }
        public string? ImgBox { get; set; }
        public bool Visible { get; set; }

        // Display Continentes 
        public IEnumerable<SelectListItem> Continentes { get; set; }


        //Collect Multiple itens
        public string[] SelectedContinentes { get; set; } = Array.Empty<string>();
    }
}
