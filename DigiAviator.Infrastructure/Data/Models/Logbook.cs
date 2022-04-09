using DigiAviator.Infrastructure.Data.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Logbook
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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
        public IList<Flight> Flights { get; set; } = new List<Flight>();

        [ForeignKey(nameof(Holder))]
        public string HolderId { get; set; }
        public ApplicationUser Holder { get; set; }
    }
}
