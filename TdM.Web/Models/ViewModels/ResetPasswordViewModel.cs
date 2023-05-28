using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace TdM.Web.Models.ViewModels;

public class ResetPasswordViewModel
{
    public string UserId { get; set; }
    public string Token { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$", ErrorMessage = "Password must meet the following criteria:<br>- Have at least 6 characters.<br>- Contain at least one digit.<br>- Contain at least one lowercase letter.<br>- Contain at least one uppercase letter.<br>- Contain at least one non-alphanumeric character.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
