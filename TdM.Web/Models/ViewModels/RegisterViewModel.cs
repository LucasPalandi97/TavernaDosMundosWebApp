using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [PasswordValidation(ErrorMessage = "Invalid Password")]
    public string Password { get; set; }
}
