using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

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

            // Check if the new email already exists
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
                    TempData["RegisterConfirmed"] = "Account registered successfully, please proceed to Login.";
                    return RedirectToAction("Login");
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
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null /*&& await userManager.IsEmailConfirmedAsync(user)*/)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                // Send the password reset email
                await SendPasswordResetEmail(user.Email, callbackUrl);

                // Optionally, you can display a message to inform the user about the password reset email being sent
                TempData["PasswordResetEmailSent"] = true;
                return View(model);
            }
            // Handle the case when the user is not found or the email is not confirmed
            ModelState.AddModelError("Email", "Password reset request failed. Please make sure you have entered the correct email address.");
        }
        return View(model);
    }

    public async Task<bool> SendPasswordResetEmail(string email, string resetToken)
    {
        // Create the email message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Taverna Dos Mundos", "noreply.tavernadosmundos@gmail.com"));
        message.To.Add(new MailboxAddress("", email)); // Recipient's email address
        message.Subject = "Password Reset";
        message.Body = new TextPart("plain")
        {
            Text = $@"Reset your password at the following link:<br>
<a href=""{resetToken}"">{resetToken}</a><br>


If you didn't request this password reset, please ignore this message.<br>

Thanks, Taverna dos Mundos<br>

Please don't reply to this message. It was sent from an address that doesn't accept incoming email."
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);// Connect to the SMTP server with TLS
            client.Authenticate("noreply.tavernadosmundos@gmail.com", "tj0qk741AWnNfhKm");

            // Send the email
            await client.SendAsync(message);
            client.Disconnect(true);
        }


        return true;
    }

    [HttpGet]
    public IActionResult ResetPassword(string userId, string token)
    {
        var model = new ResetPasswordViewModel
        {
            UserId = userId,
            Token = token
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {      
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset request.");
                return View(model);
            }

            var resetPasswordResult = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (resetPasswordResult.Succeeded)
            {
                // Password reset successful, you can redirect the user to a success page or the login page
                TempData["RegisterConfirmed"] = "Password reseted successfully, please proceed to Login.";
                return RedirectToAction("Login");
            }

            foreach (var error in resetPasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
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