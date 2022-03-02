using ProjectReferencesDemo.Services.Models;

namespace ProjectReferencesDemo.Web.Models
{
    public class SaveCustomerViewModel
    {
        public Customer Customer { get; set; }

        public List<CustomerType> CustomerTypes { get; set; }

        public int CustomerTypeId { get; set; }
    }
}
