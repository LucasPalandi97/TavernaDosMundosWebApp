using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels;

public class UserViewModel
{
    [BindNever]
    public List<User> Users { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$", ErrorMessage = "Password must meet the following criteria:<br>- Have at least 6 characters.<br>- Contain at least one digit.<br>- Contain at least one lowercase letter.<br>- Contain at least one uppercase letter.<br>- Contain at least one non-alphanumeric character.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not matches")]
    public string ConfirmPassword { get; set; }

    public bool isAdmin { get; set; }

}
