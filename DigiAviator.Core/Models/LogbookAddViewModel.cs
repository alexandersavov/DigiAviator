using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class LogbookAddViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Address { get; set; }

    }
}
