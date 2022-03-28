using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class MedicalAddViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string IssuingAuthorithy { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string LicenseNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string MedicalNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Nationality { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }
    }
}
