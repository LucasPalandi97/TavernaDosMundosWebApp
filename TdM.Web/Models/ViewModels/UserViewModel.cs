using System.ComponentModel.DataAnnotations;

namespace TdM.Web.Models.ViewModels;

public class UserViewModel
{
    public List<User> Users { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [PasswordValidation(ErrorMessage = "Invalid Password")]
    public string Password { get; set; }

    public bool isAdmin { get; set; }

}
