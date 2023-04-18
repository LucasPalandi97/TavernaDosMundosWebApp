
using TdM.Database.Models.Domain;
namespace TdM.Web.Models.ViewModels;

public class MundoViewModel
{
    public Mundo Mundo { get; set; }
    public string MundoUrl { get; set; }
    public IEnumerable<Mundo> Mundos { get; set; }
}
