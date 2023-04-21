using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TdM.Database.Models.Domain.Enums;

public enum Autor
{
    [Display(Name = "Bruno Cordon")]
    Bruno,
    [Display(Name = "Emerson Delfin Luiz Neves")]
    Emerson,
    [Display(Name = "Felipe Mirabelli Jacomini")]
    Felipe,
    [Display(Name = "Lucas Vinicius Palandi")]
    Lucas,
    [Display(Name = "Rodrigo Bueno Guedes")]
    Rodrigo
}
