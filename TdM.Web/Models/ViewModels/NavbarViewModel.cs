using TdM.Database.Models.Domain;

namespace TdM.Web.Models.ViewModels;

public class NavbarViewModel
{
    public Mundo Mundo { get; set; }
    public string MundoUrlHandle { get; set; }
    public IEnumerable<Continente> Continentes { get; set; }
    public IEnumerable<Regiao> Regioes { get; set; }
    public IEnumerable<Personagem> Personagens { get; set; }
    public IEnumerable<Criatura> Criaturas { get; set; }
    public IEnumerable<Povo> Povos { get; set; }
    public IEnumerable<Conto> Contos { get; set; }
}
