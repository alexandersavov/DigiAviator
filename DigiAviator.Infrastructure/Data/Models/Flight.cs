using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiAviator.Infrastructure.Data.Models
{
    public class Flight
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime DateOfFlight { get; set; }

        [Required]
        [StringLength(4)]
        public string DepartureAirportICAO { get; set; }

        [Required]
        public TimeSpan DepartureTimeUTC { get; set; }

        [Required]
        [StringLength(4)]
        public string ArrivalAirportICAO { get; set; }

        [Required]
        public TimeSpan ArrivalTimeUTC { get; set; }

        [Required]
        [StringLength(10)]
        public string AircraftType { get; set; }

        [Required]
        [StringLength(8)]
        public string AircraftRegistration { get; set; }

        [Required]
        public TimeSpan TotalFlightTime { get; set; }

        [Required]
        [StringLength(60)]
        public string PilotInCommandName { get; set; }

        [Required]
        [Range(0, 40)]
        public int LandingsDay { get; set; }

        [Required]
        [Range(0, 40)]
        public int LandingsNight { get; set; }

        [Required]
        public TimeSpan PilotInCommandFunctionTime { get; set; }

        [Required]
        public TimeSpan CopilotFunctionTime { get; set; }

        [Required]
        public TimeSpan DualFunctionTime { get; set; }

        [Required]
        public TimeSpan InstructorFunctionTime { get; set; }

        [ForeignKey(nameof(Logbook))]
        public Guid LogbookId { get; set; }
        public Logbook Logbook { get; set; }
    }
}
