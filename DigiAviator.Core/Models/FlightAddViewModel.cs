using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class FlightAddViewModel
    {
        [Required]
        public DateTime DateOfFlight { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string DepartureAirportICAO { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan DepartureTimeUTC { get; set; }

        [Required]
        [StringLength(4)]
        public string ArrivalAirportICAO { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00", "23:59")]
        public TimeSpan ArrivalTimeUTC { get; set; }

        [Required]
        [StringLength(10)]
        public string AircraftType { get; set; }

        [Required]
        [StringLength(8)]
        public string AircraftRegistration { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00", "20:00")]
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
        [Range(typeof(TimeSpan), "00:00", "14:00")]
        public TimeSpan PilotInCommandFunctionTime { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00", "14:00")]
        public TimeSpan CopilotFunctionTime { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00", "14:00")]
        public TimeSpan DualFunctionTime { get; set; }

        [Required]
        [Range(typeof(TimeSpan), "00:00", "14:00")]
        public TimeSpan InstructorFunctionTime { get; set; }
    }
}
