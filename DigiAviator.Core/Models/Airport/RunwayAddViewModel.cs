using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Models
{
    public class RunwayAddViewModel
    {
        [Required]
        [StringLength(3)]
        public string Designation { get; set; }

        [Required]
        [StringLength(3)]
        public string TrueCourse { get; set; }

        [Required]
        [StringLength(3)]
        public string MagneticCourse { get; set; }

        [Range(0, 20000)]
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
