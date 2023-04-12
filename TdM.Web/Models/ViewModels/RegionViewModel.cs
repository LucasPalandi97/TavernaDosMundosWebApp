
using TdM.Database.Models.Domain;

namespace TdM.Web.Models.ViewModels;

public class RegionViewModel
{
    public IEnumerable<Continente> Continentes { get; set; }

    public IEnumerable<Regiao> Regioes { get; set; }
}
