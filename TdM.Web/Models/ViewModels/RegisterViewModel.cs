using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [StringLength(12, ErrorMessage = "The Username must be a maximum of 12 characters.")]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public bool IsEmailVerified { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$", ErrorMessage = "Password must meet the following criteria:<br>- Have at least 6 characters.<br>- Contain at least one digit.<br>- Contain at least one lowercase letter.<br>- Contain at least one uppercase letter.<br>- Contain at least one non-alphanumeric character.")]

    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not matches")]
    public string ConfirmPassword { get; set; }
}
