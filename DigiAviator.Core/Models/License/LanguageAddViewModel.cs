using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class LanguageAddViewModel
    {
        [Required]
        [StringLength(20)]
        public string LanguageName { get; set; }

        [Required]
        [Range(1, 6)]
        public int IcaoLevel { get; set; }

        [Required]
        public string ValidUntil { get; set; }
    }
}
