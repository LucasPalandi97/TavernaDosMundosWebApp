using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
