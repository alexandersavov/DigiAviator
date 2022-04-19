using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
	public class EmailSubmitViewModel
	{
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(80)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500)]
        public string Body { get; set; }
    }
}
