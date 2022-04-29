using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class FitnessTypeAddViewModel
    {
        [Required]
        [StringLength(10)]
        public string FitnessClass { get; set; }

        [Required]
        public string ValidUntil { get; set; }
    }
}
