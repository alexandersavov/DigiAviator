using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class FitnessType
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(10)]
        public string FitnessClass { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        [ForeignKey(nameof(Medical))]
        public Guid MedicalId { get; set; }
        public Medical Medical { get; set; }
    }
}
