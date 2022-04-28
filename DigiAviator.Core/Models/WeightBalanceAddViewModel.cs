using System.ComponentModel.DataAnnotations;

namespace DigiAviator.Core.Models
{
    public class WeightBalanceAddViewModel
    {
        [Required]
        [Range(0, 13000)]
        public double BasicEmptyWeight { get; set; }
        [Required]
        [Range(0, 200)]
        public double BasicEmptyWeightArm { get; set; }

        [Required]
        [Range(0, 300)]
        public double FrontLeftSeat { get; set; }
        [Required]
        [Range(0, 120)]
        public double FrontLeftSeatArm { get; set; }

        [Required]
        [Range(0, 300)]
        public double FrontRightSeat { get; set; }
        [Required]
        [Range(0, 120)]
        public double FrontRightSeatArm { get; set; }

        [Required]
        [Range(0, 300)]
        public double RearLeftSeat { get; set; }
        [Required]
        [Range(0, 160)]
        public double RearLeftSeatArm { get; set; }

        [Required]
        [Range(0, 300)]
        public double RearRightSeat { get; set; }
        [Required]
        [Range(0, 160)]
        public double RearRightSeatArm { get; set; }

        [Required]
        [Range(0, 100)]
        public double BaggageAreaOne { get; set; }
        [Required]
        [Range(0, 180)]
        public double BaggageAreaOneArm { get; set; }

        [Required]
        [Range(0, 80)]
        public double BaggageAreaTwo { get; set; }
        [Required]
        [Range(0, 190)]
        public double BaggageAreaTwoArm { get; set; }

        [Required]
        [Range(0, 100)]
        public double TotalFuel { get; set; }
        [Required]
        [Range(40, 180)]
        public double TotalFuelArm { get; set; }

        [Required]
        [Range(0, 20)]
        public double TaxiFuel { get; set; }
        [Required]
        [Range(40, 180)]
        public double TaxiFuelArm { get; set; }

        [Required]
        [Range(0, 80)]
        public double TripFuel { get; set; }
        [Required]
        [Range(40, 180)]
        public double TripFuelArm { get; set; }

    }
}
