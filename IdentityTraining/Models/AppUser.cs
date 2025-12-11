using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityTraining.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
    }
}
