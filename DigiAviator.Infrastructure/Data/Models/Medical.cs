using DigiAviator.Infrastructure.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Medical
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(30)]
        public string IssuingAuthorithy { get; set; }

        [Required]
        [StringLength(25)]
        public string LicenseNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string MedicalNumber { get; set; }

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
        [StringLength(30)]
        public string Nationality { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public IList<Limitation> Limitations { get; set; } = new List<Limitation>();

        [Required]
        public DateTime IssuedOn { get; set; }

        [Required]
        public IList<FitnessType> FitnessTypes { get; set; } = new List<FitnessType>();


        [ForeignKey(nameof(Holder))]
        public string HolderId { get; set; }
        public ApplicationUser Holder { get; set; }
    }
}
