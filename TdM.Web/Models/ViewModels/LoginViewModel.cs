using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    public string UsernameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
}
