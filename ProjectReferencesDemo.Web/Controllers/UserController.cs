using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectReferencesDemo.Services.Data;
using ProjectReferencesDemo.Web.Models;

namespace ProjectReferencesDemo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<UsersViewModel> users = new();
            var allRoles = context.Roles.ToList();

            foreach (var user in context.Users)
            {
                var roles = new List<EditRoleViewModel>();
                foreach (var role in allRoles)
                {
                    roles.Add(new(
                        role, 
                        await userManager.IsInRoleAsync(user, role.Name)
                    ));
                }

                users.Add(new()
                {
                    User = user,
                    Roles = roles
                });
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(List<UsersViewModel> users)
        {
            return RedirectToAction("Index");
        }
    }
}
