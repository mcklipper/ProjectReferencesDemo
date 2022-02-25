using ProjectReferencesDemo.Services.Models;

namespace ProjectReferencesDemo.Web.Models
{
    public class CreateCustomerViewModel
    {
        public Customer Customer { get; set; }

        public List<CustomerType> CustomerTypes { get; set; }

        public int CustomerTypeId { get; set; }
    }
}
