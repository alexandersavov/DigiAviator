using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Models
{
    public class WeightBalanceAddViewModel
    {
        public double BasicEmptyWeight { get; set; }
        public double BasicEmptyWeightArm { get; set; }

        public double FrontLeftSeat { get; set; }
        public double FrontLeftSeatArm { get; set; }

        public double FrontRightSeat { get; set; }
        public double FrontRightSeatArm { get; set; }

        public double RearLeftSeat { get; set; }
        public double RearLeftSeatArm { get; set; }

        public double RearRightSeat { get; set; }
        public double RearRightSeatArm { get; set; }

        public double BaggageAreaOne { get; set; }
        public double BaggageAreaOneArm { get; set; }

        public double BaggageAreaTwo { get; set; }
        public double BaggageAreaTwoArm { get; set; }


        public double TotalFuel { get; set; }
        public double TotalFuelArm { get; set; }

        public double TaxiFuel { get; set; }
        public double TaxiFuelArm { get; set; }

        public double TripFuel { get; set; }
        public double TripFuelArm { get; set; }

    }
}
