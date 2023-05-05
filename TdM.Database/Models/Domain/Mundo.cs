namespace TdM.Database.Models.Domain;
using TdM.Database.Models.Domain.Enums;

public class Mundo
{

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CurtaDescricao { get; set; }
    public string Descricao { get; set; }
    public Autor Autor { get; set; }
    public string? ImgBox { get; set; }
    public DateTime PublishedDate { get; set; }
    public string UrlHandle { get; set; }
    public bool Visible { get; set; }


    public ICollection<Continente>? Continentes { get; set; }
    public ICollection<Regiao>? Regioes { get; set; }
    public ICollection<Personagem>? Personagens { get; set; }
    public ICollection<Povo>? Povos { get; set; }
    public ICollection<Criatura>? Criaturas { get; set; }
    public ICollection<Conto>? Contos { get; set; }


}


