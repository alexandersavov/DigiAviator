using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Airport
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string IcaoIdentifier { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Latitude { get; set; }

        [Required]
        [Range(-90, 90)]
        public double Longitude { get; set; }

        [Required]
        public int Elevation { get; set; }

        public IList<Runway> Runways { get; set; } = new List<Runway>();
    }
}
