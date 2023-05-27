using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace TdM.Web.Models.ViewModels;

public class ChangePasswordViewModel
{
    public string Username { get; set; }

    [EmailAddress]
    [Display(Name = "New Email")]
    public string? NewEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string CurrentPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$", ErrorMessage = "Password must meet the following criteria:<br>- Have at least 6 characters.<br>- Contain at least one digit.<br>- Contain at least one lowercase letter.<br>- Contain at least one uppercase letter.<br>- Contain at least one non-alphanumeric character.")]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm New Password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmNewPassword { get; set; }

    public string EmailChangeConfirmation { get; set; }
    public string PasswordChangeConfirmation { get; set; }

}
