using Microsoft.AspNetCore.Identity;

namespace ProjectReferencesDemo.Web.Models
{
    public class EditRoleViewModel
    {
        public IdentityRole Role { get; set; }
        public bool IsInRole { get; set; }

        public EditRoleViewModel(IdentityRole role, bool isInRole)
        {
            Role = role;
            IsInRole = isInRole;
        }

        public EditRoleViewModel()
        {

        }
    }
}
