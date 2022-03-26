using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Models
{
    public class CalculatedWeightBalanceViewModel
    {
        public double ZeroFuelWeight { get; set; }
        public double ZeroFuelWeightArm { get; set; }
        public double ZeroFuelWeightMoment { get; set; }

        public double RampWeight { get; set; }
        public double RampWeightArm { get; set; }
        public double RampWeightMoment { get; set; }

        public double TakeoffWeight { get; set; }
        public double TakeoffWeightArm { get; set; }
        public double TakeoffWeightMoment { get; set; }

        public double LandingWeight { get; set; }
        public double LandingWeightArm { get; set; }
        public double LandingWeightMoment { get; set; }

    }
}
