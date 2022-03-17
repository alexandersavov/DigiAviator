using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(15)]
        public string ClassType { get; set; }

        [Required]
        public DateTime Validity { get; set; }


        [ForeignKey(nameof(License))]
        public Guid LicenseId { get; set; }
        public License License { get; set; }
    }
}
