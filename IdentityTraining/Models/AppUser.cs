using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityTraining.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}