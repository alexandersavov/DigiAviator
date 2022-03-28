using DigiAviator.Infrastructure.Data.Models;

namespace DigiAviator.Core.Models
{
    public class MedicalViewModel
    {
        public string Id { get; set; } 

        public string IssuingAuthorithy { get; set; }

        public string LicenseNumber { get; set; }

        public string MedicalNumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public DateTime BirthDate { get; set; }

        public IList<LimitationListViewModel> Limitations { get; set; } = new List<LimitationListViewModel>();

        public DateTime IssuedOn { get; set; }

        public IList<FitnessTypeListViewModel> FitnessTypes { get; set; } = new List<FitnessTypeListViewModel>();

        public string HolderId { get; set; }
    }
}
