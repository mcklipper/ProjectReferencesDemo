using Microsoft.AspNetCore.Identity;

namespace ProjectReferencesDemo.Web.Models
{
    public class UsersViewModel
    {
        public IdentityUser User { get; set; }

        public List<EditRoleViewModel> Roles { get; set; }
    }
}
