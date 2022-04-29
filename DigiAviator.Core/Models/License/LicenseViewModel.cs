namespace DigiAviator.Core.Models
{
    public class LicenseViewModel
    {
        public string Id { get; set; }

        public string IssueState { get; set; }

        public string LicenseNumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Nationality { get; set; }

        public string IssuingAuthority { get; set; }

        public string CountryCode { get; set; }

        public string DateOfInitialIssue { get; set; }

        public string TitleOfLicense { get; set; }

        public IList<LanguageListViewModel> Languages { get; set; } = new List<LanguageListViewModel>();

        public string BirthDate { get; set; }

        public IList<RatingListViewModel> Ratings { get; set; } = new List<RatingListViewModel>();

        public string HolderId { get; set; }
    }
}
