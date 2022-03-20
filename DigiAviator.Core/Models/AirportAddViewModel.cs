using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Models
{
    public class AirportAddViewModel
    {
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
    }
}
