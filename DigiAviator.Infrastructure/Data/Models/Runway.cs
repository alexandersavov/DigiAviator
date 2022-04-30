using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Runway
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(License))]
        public Guid AirportId { get; set; }
        public Airport Airport { get; set; }

        [Required]
        [StringLength(3)]
        public string Designation { get; set; }

        [Required]
        public string TrueCourse { get; set; }

        [Required]
        public string MagneticCourse { get; set; }

        [Range(0,20000)]
        public int Length { get; set; }

        [Range(0, 100)]
        public int Width { get; set; }

        [Required]
        public double Slope { get; set; }

        [Range(0, 20000)]
        public int TORA { get; set; }

        [Range(0, 20000)]
        public int TODA { get; set; }

        [Range(0, 20000)]
        public int ASDA { get; set; }

        [Range(0, 20000)]
        public int LDA { get; set; }
    }
}
