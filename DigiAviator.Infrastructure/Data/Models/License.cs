using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class License
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(60)]
        public string IssueState { get; set; }

        [Required]
        [StringLength(25)]
        public string LicenseNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(60)]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string Nationality { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(30)]
        public string IssuingAuthorithy { get; set; }

        [Required]
        [StringLength(10)]
        public string TitleOfLicense { get; set; }

        [Required]
        public DateTime DateOfInitialIssue { get; set; }

        [Required]
        [StringLength(6)]
        public string CountryCode { get; set; }

        [Required]
        public IList<Language> LanguageProficiency { get; set; } = new List<Language>();

        [Required]
        public IList<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
