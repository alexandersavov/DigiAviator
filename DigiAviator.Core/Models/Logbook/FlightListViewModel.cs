namespace DigiAviator.Core.Models
{
    public class FlightListViewModel
    {
        public string Id { get; set; }
        public string DateOfFlight { get; set; }
        public string DepartureAirportICAO { get; set; }
        public string DepartureTimeUTC { get; set; }
        public string ArrivalAirportICAO { get; set; }
        public string ArrivalTimeUTC { get; set; }
        public string AircraftType { get; set; }
        public string AircraftRegistration { get; set; }
        public string TotalFlightTime { get; set; }
        public string PilotInCommandName { get; set; }
        public int LandingsDay { get; set; }
        public int LandingsNight { get; set; }
        public string PilotInCommandFunctionTime { get; set; }
        public string CopilotFunctionTime { get; set; }
        public string DualFunctionTime { get; set; }
        public string InstructorFunctionTime { get; set; }
    }
}
