using System.ComponentModel.DataAnnotations;
using TdM.Database.Models.Domain.Enums;

namespace TdM.Web.Models.ViewModels
{
    public class EditMundoRequest
    {
        public Guid MundoId { get; set; }
     
        public string Nome { get; set; }
     
        public string Descricao { get; set; }

        public Autor Autor { get; set; }

    }
}
