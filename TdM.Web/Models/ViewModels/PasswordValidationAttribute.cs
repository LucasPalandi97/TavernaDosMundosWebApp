
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TdM.Web.Models.ViewModels;
public class PasswordValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string? password = value as string;

        if (string.IsNullOrEmpty(password))
            return false;

        if (password.Length < 6)
        {
            ErrorMessage = "Password must have at least 6 characters.";
            return false;
        }

        if (!password.Any(char.IsDigit))
        {
            ErrorMessage = "Password must contain at least one digit.";
            return false;
        }

        if (!password.Any(char.IsLower))
        {
            ErrorMessage = "Password must contain at least one lowercase letter.";
            return false;
        }

        if (!password.Any(char.IsUpper))
        {
            ErrorMessage = "Password must contain at least one uppercase letter.";
            return false;
        }

        if (!password.Any(c => !char.IsLetterOrDigit(c)))
        {
            ErrorMessage = "Password must contain at least one non-alphanumeric character.";
            return false;
        }

        return true;
    }
}
