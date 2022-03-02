using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectReferencesDemo.Services.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfRegistration { get; set; }

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        public bool Gender { get; set; }

        public CustomerType CustomerType { get; set; }

        public IdentityUser User { get; set; }
    }
}
