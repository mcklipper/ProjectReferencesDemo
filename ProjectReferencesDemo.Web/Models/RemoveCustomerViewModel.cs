using ProjectReferencesDemo.Services.Models;

namespace ProjectReferencesDemo.Web.Models
{
    public class RemoveCustomerViewModel
    {
        public Customer Customer { get; set; }
        public bool SafeDelete { get; set; } = true;
        public bool IsAdmin { get; set; } = false;

    }
}
