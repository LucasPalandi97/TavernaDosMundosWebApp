using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TdM.Web.Models.ViewModels;
using TdM.Web.Repositories;


namespace TdM.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUsersController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly UserManager<IdentityUser> userManager;

    public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
    {
        this.userRepository = userRepository;
        this.userManager = userManager;
    }

    public async Task<IActionResult> List()
    {
        var users = await userRepository.GetAllAsync(1, 10);

        var usersViewModel = new UserViewModel();
        usersViewModel.Users = new List<User>();
        foreach (var user in users)
        {
            usersViewModel.Users.Add(new Models.ViewModels.User
            {
                Id = Guid.Parse(user.Id),
                Username = user.UserName,
                EmailAdress = user.Email
            });
        }

        return View(usersViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> List(UserViewModel request)
    {   
        ModelState.Remove("Users"); // Remove the Users property from ModelState

        if (ModelState.IsValid)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email

            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    //Assign roles to this user
                    var roles = new List<string> { "User" };

                    if (request.isAdmin)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }
        }
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user is not null)
        {
            var identitiyResult = await userManager.DeleteAsync(user);

            if (identitiyResult is not null && identitiyResult.Succeeded)
            {
                return RedirectToAction("List", "AdminUsers");
            }
        }
        return View();
    }
}
