using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
	public class EmailSubmitViewModel
	{
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 50)]
        public string Body { get; set; }
    }
}
