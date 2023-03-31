using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TdM.Web.Models.Domain.Enums;

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
