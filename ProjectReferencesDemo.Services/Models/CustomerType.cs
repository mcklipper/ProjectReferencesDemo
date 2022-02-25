using System.ComponentModel.DataAnnotations;

namespace ProjectReferencesDemo.Services.Models
{
    public class CustomerType
    {
        public int Id { get; set; }

        [StringLength(16)]
        public string Name { get; set; }
    }
}
