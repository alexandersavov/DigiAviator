using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Language
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(20)]
        public string LanguageName { get; set; }

        [Required]
        [Range(1, 6)]
        public int IcaoLevel { get; set; }

        [Required]
        public DateTime DateOfValidity { get; set; }

        [ForeignKey(nameof(License))]
        public Guid LicenseId { get; set; }
        public License License { get; set; }
    }
}
