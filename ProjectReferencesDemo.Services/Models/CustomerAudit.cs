using Microsoft.AspNetCore.Identity;
using ProjectReferencesDemo.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectReferencesDemo.Services.Data
{
    public class CustomerAudit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfRegistration { get; set; }

        public DateTime DateOfQuit { get; set; }

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        public bool Gender { get; set; }

        public CustomerType CustomerType { get; set; }

        public IdentityUser User { get; set; }
    }
}