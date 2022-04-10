using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class FlightAddViewModel
    {
        [Required]
        public string DateOfFlight { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string DepartureAirportICAO { get; set; }

        [Required]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 23:59")]
        public string DepartureTimeUTC { get; set; }

        [Required]
        [StringLength(4)]
        public string ArrivalAirportICAO { get; set; }

        [Required]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 23:59")]
        public string ArrivalTimeUTC { get; set; }

        [Required]
        [StringLength(10)]
        public string AircraftType { get; set; }

        [Required]
        [StringLength(8)]
        public string AircraftRegistration { get; set; }

        [Required]
        [RegularExpression(@"^([0-1][0-9]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 23:59")]
        public string TotalFlightTime { get; set; }

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
        [RegularExpression(@"^([0-1][0-4]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 14:00")]
        public string PilotInCommandFunctionTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1][0-4]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 14:00")]
        public string CopilotFunctionTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1][0-4]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 14:00")]
        public string DualFunctionTime { get; set; }

        [Required]
        [RegularExpression(@"^([0-1][0-4]|2[0-3]):?([0-5][0-9])$", ErrorMessage = "Accepted values between 00:00 and 14:00")]
        public string InstructorFunctionTime { get; set; }
    }
}
