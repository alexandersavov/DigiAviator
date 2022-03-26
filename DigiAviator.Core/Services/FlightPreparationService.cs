using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Services
{
    public class FlightPreparationService : IFlightPreparationService
    {
        public CalculatedWeightBalanceViewModel CalculateWeightBalance(WeightBalanceAddViewModel model)
        {
            //Calculate plane, passengers and cargo moments//
            double basicEmptyWeightMoment = CalculateMoment(model.BasicEmptyWeight, model.BasicEmptyWeightArm);
            double frontLeftSeatMoment = CalculateMoment(model.FrontLeftSeat, model.FrontLeftSeatArm);
            double frontRightSeatMoment = CalculateMoment(model.FrontRightSeat, model.FrontRightSeatArm);
            double rearLeftSeatMoment = CalculateMoment(model.RearLeftSeat, model.RearLeftSeatArm);
            double rearRightSeatMoment = CalculateMoment(model.RearRightSeat, model.RearRightSeatArm);
            double baggageAreaOneMoment = CalculateMoment(model.BaggageAreaOne, model.BaggageAreaOneArm);
            double baggageAreaTwoMoment = CalculateMoment(model.BaggageAreaTwo, model.BaggageAreaTwoArm);

            //Calculate zero fuel weight, moment and arm//
            double zeroFuelWeight = model.BasicEmptyWeight + model.FrontLeftSeat + model.FrontRightSeat + model.RearLeftSeat + model.RearRightSeat + model.BaggageAreaOne + model.BaggageAreaTwo;
            double zeroFuelWeightMoment = basicEmptyWeightMoment + frontLeftSeatMoment + frontRightSeatMoment + rearLeftSeatMoment + rearRightSeatMoment + baggageAreaOneMoment + baggageAreaTwoMoment;

            double zeroFuelWeightArm = CalculateArm(zeroFuelWeight, zeroFuelWeightMoment);

            //Calculate total fuel moment//
            double totalFuelMoment = CalculateMoment(AvgasGalToLbs(model.TotalFuel), model.TotalFuelArm);

            //Calculate ramp weight, moment and arm//
            double rampWeight = zeroFuelWeight + AvgasGalToLbs(model.TotalFuel);
            double rampWeightMoment = zeroFuelWeightMoment + totalFuelMoment;

            double rampWeightArm = CalculateArm(rampWeight, rampWeightMoment);

            //Calculate taxi fuel moment//
            double taxiFuelMoment = CalculateMoment(AvgasGalToLbs(model.TaxiFuel), model.TaxiFuelArm);

            //Calculate takeoff weight, moment and arm//
            double takeoffWeight = rampWeight - AvgasGalToLbs(model.TaxiFuel);
            double takeoffWeightMoment = rampWeightMoment - taxiFuelMoment;

            double takeoffWeightArm = CalculateArm(takeoffWeight, takeoffWeightMoment);

            //Calculate trip fuel moment//
            double tripFuelMoment = CalculateMoment(AvgasGalToLbs(model.TripFuel), model.TripFuelArm);

            //Calculate landing weight, moment and arm//
            double landingWeight = takeoffWeight - AvgasGalToLbs(model.TripFuel);
            double landingWeightMoment = takeoffWeightMoment - tripFuelMoment;

            double landingWeightArm = CalculateArm(landingWeight, landingWeightMoment);

            //Create view model//
            var calculatedWeightBalance = new CalculatedWeightBalanceViewModel
            {
                ZeroFuelWeight = Math.Round(zeroFuelWeight,2),
                ZeroFuelWeightArm = Math.Round(zeroFuelWeightArm,2),
                ZeroFuelWeightMoment = Math.Round(zeroFuelWeightMoment,2),
                RampWeight = Math.Round(rampWeight,2),
                RampWeightArm = Math.Round(rampWeightArm,2),
                RampWeightMoment = Math.Round(rampWeightMoment,2),
                TakeoffWeight = Math.Round(takeoffWeight,2),
                TakeoffWeightArm = Math.Round(takeoffWeightArm,2),
                TakeoffWeightMoment = Math.Round(takeoffWeightMoment,2),
                LandingWeight = Math.Round(landingWeight,2),
                LandingWeightArm = Math.Round(landingWeightArm,2),
                LandingWeightMoment = Math.Round(landingWeightMoment,2)
            };

            return calculatedWeightBalance;
        }

        private double CalculateMoment(double weight, double arm)
        {
            return weight * arm;
        }

        private double CalculateArm(double weight, double moment)
        {
            return moment / weight;
        }

        private double AvgasGalToLbs(double galonsOfFuel)
        {
            return galonsOfFuel * 6;
        }
    }
}
