using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class RatingAddViewModel
    {
        [Required]
        [StringLength(15)]
        public string ClassType { get; set; }

        [Required]
        public string ValidUntil { get; set; }
    }
}
