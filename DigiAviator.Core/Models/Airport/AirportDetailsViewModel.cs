namespace DigiAviator.Core.Models
{
    public class AirportDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string IcaoIdentifier { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Elevation { get; set; }

        public List<RunwayListViewModel> Runways { get; set; } = new List<RunwayListViewModel>();
    }
}
