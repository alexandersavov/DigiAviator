using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Limitation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required]
        [StringLength(6)]
        public string LimitationCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [ForeignKey(nameof(Medical))]
        public Guid MedicalId { get; set; }
        public Medical Medical { get; set; }
    }
}
