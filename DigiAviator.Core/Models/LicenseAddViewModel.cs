using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class LicenseAddViewModel
    {

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string IssueState { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string LicenseNumber { get; set; }

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
        [StringLength(60, MinimumLength = 2)]
        public string Address { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nationality { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string IssuingAuthorithy { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string TitleOfLicense { get; set; }

        [Required]
        public string DateOfInitialIssue { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 1)]
        public string CountryCode { get; set; }
    }
}
