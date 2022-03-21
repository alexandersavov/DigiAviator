using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Infrastructure.Data.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }
    }
}
