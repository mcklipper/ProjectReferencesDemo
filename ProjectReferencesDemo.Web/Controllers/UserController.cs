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

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(IdentityUser user)
        {
            var userInDb = await userManager.FindByIdAsync(user.Id);

            if (userInDb == null)
                return BadRequest();

            await userManager.DeleteAsync(userInDb);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditRoles(string id, UsersViewModel vm)
        {
            var user = await userManager.FindByIdAsync(id);

            foreach (var rolevm in vm.Roles)
            {
                bool isInRoleInDb = await userManager.IsInRoleAsync(user, rolevm.Role.Name);

                if (rolevm.IsInRole && !isInRoleInDb)
                    await userManager.AddToRoleAsync(user, rolevm.Role.Name);

                else if (!rolevm.IsInRole && isInRoleInDb)
                    await userManager.RemoveFromRoleAsync(user, rolevm.Role.Name);
            }

            return RedirectToAction("Index");
        }
    }
}
