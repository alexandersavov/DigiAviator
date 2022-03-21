namespace DigiAviator.Core.Models
{
    public class RunwayListViewModel
    {
        public string Designation { get; set; }

        public string TrueCourse { get; set; }

        public string MagneticCourse { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public double Slope { get; set; }

        public int TORA { get; set; }

        public int TODA { get; set; }

        public int ASDA { get; set; }

        public int LDA { get; set; }
    }
}
