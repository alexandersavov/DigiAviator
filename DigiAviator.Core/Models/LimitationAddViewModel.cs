using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class LimitationAddViewModel
    {
        [Required]
        [StringLength(6)]
        public string LimitationCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
