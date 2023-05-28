using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;

namespace TdM.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        ModelState.Remove("RegisterConfirmation"); // Remove the RegisterConfirmation property from ModelState
        if (ModelState.IsValid)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };

            // Check if the new username already exists
            var existingUsername = await userManager.FindByNameAsync(registerViewModel.Username);
            if (existingUsername != null)
            {
                ModelState.AddModelError("Username", "This username is already in use.");
                return View(registerViewModel);
            }

            // Check if the new email already exists
            var existingEmail = await userManager.FindByEmailAsync(registerViewModel.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "This email is already in use.");
                return View(registerViewModel);
            }

            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                // Assign this user the "User" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                if (roleIdentityResult.Succeeded)
                {
                    TempData["RegistrationSuccess"] = true;
                    return View("Login");
                }
            }
        }
        return View(registerViewModel);
    }

    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        var model = new LoginViewModel
        {
            ReturnUrl = ReturnUrl
        };

        if (TempData.ContainsKey("RegistrationSuccess") && (bool)TempData["RegistrationSuccess"])
        {
            ViewBag.RegistrationSuccess = true;
            TempData.Remove("RegistrationSuccess"); // Remove the TempData flag to avoid persisting it
        }
        else
        {
            ViewBag.RegistrationSuccess = false;
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        // Check if the username exists
        var user = await userManager.FindByNameAsync(loginViewModel.Username);
        if (user == null)
        {
            ModelState.AddModelError("Password", "Invalid username or password");
            return View();
        }

        // Attempt to sign in the user with the provided username and password
        var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

        if (signInResult.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
            {
                return Redirect(loginViewModel.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("Password", "Invalid username or password");
            return View();
        }
    }

    [HttpGet]
    [Route("/Profile")]
    public async Task<IActionResult> Profile()
    {
        if (signInManager.IsSignedIn(User))
        {
            var user = await userManager.GetUserAsync(User);
            var viewModel = new ChangePasswordViewModel
            {
                Username = user.UserName,
                NewEmail = user.Email
            };

            return View(viewModel);
        }

        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmail(ChangePasswordViewModel model)
    {
        ModelState.Remove("Username"); // Remove the Username property from ModelState
        ModelState.Remove("CurrentPassword"); // Remove the CurrentPassword property from ModelState
        ModelState.Remove("NewPassword"); // Remove the Username NewPassword from ModelState
        ModelState.Remove("ConfirmNewPassword"); // Remove the ConfirmNewPassword property from ModelState
        ModelState.Remove("EmailChangeConfirmation"); // Remove the EmailChangeConfirmation property from ModelState
        ModelState.Remove("PasswordChangeConfirmation"); // Remove the PasswordChangeConfirmation property from ModelState

        var user = await userManager.GetUserAsync(User);

        if (ModelState.IsValid)
        {
            // Change email
            if (user.Email != model.NewEmail && model.NewEmail != null)
            {
                // Check if the new email already exists
                var existingUser = await userManager.FindByEmailAsync(model.NewEmail);
                if (existingUser != null)
                {
                    ModelState.AddModelError("NewEmail", "This email is already in use.");
                    model.Username = user.UserName;
                    model.NewEmail = user.Email;
                    return View("Profile", model);
                }

                // Update the email
                var setEmailResult = await userManager.SetEmailAsync(user, model.NewEmail);

                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError("NewEmail", error.Description);
                    }
                    model.Username = user.UserName;
                    model.NewEmail = user.Email;

                    return View("Profile", model);
                }

                // Email change successful
                model.EmailChangeConfirmation = "Email successfully changed.";
            }
        }
        model.Username = user.UserName;
        model.NewEmail = user.Email;
        return View("Profile", model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        ModelState.Remove("Username"); // Remove the Username property from ModelState
        ModelState.Remove("EmailChangeConfirmation"); // Remove the EmailChangeConfirmation property from ModelState
        ModelState.Remove("PasswordChangeConfirmation"); // Remove the PasswordChangeConfirmation property from ModelState

        var user = await userManager.GetUserAsync(User);
        if (ModelState.IsValid)
        {
            // Verify the current password
            var isCurrentPasswordValid = await userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!isCurrentPasswordValid)
            {
                model.Username = user.UserName;
                model.NewEmail = user.Email;
                ModelState.AddModelError("CurrentPassword", "The current password is incorrect."); // Add error message to specific field
                return View("Profile", model); // Pass the model to the Profile view with updated ModelState
            }

            // Check if the new password is the same as the current password
            if (model.NewPassword == model.CurrentPassword)
            {
                model.Username = user.UserName;
                model.NewEmail = user.Email;
                ModelState.AddModelError("NewPassword", "The new password must be different from the current password.");
                return View("Profile", model); // Pass the model to the Profile view with updated ModelState
            }

            // Change password
            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
          
            if (changePasswordResult.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
                // Password change successful
                model.PasswordChangeConfirmation = "Password successfully changed.";
                
            }

            foreach (var error in changePasswordResult.Errors)
            {
                model.Username = user.UserName;
                model.NewEmail = user.Email;
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        model.Username = user.UserName;
        model.NewEmail = user.Email;
        // Return the "Profile" view with the updated ModelState and new instance of ChangePasswordViewModel
        return View("Profile", model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public ActionResult TermsOfUse()
    {
        return View();
    }
}