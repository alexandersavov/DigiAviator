using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigiAviator.Test
{
    public class FlightPreparationServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public async Task Setup()
        {
            var serviceCollection = new ServiceCollection();

            _serviceProvider = serviceCollection
                .AddSingleton<IFlightPreparationService, FlightPreparationService>()
                .AddSingleton<IValidationService, ValidationService>()
                .BuildServiceProvider();
        }

        [Test]
        public void InvalidWeightBalanceAddViewModelMustThrow()
        {
            var weightBalanceModel = new WeightBalanceAddViewModel
            {
                BasicEmptyWeight = 14000,
                BasicEmptyWeightArm = 300,
                FrontLeftSeat = 500,
                FrontLeftSeatArm = -10,
                FrontRightSeat = 450,
                FrontRightSeatArm = 300,
                RearLeftSeat = -200,
                RearLeftSeatArm = 400,
                RearRightSeat = 500,
                RearRightSeatArm = 600,
                BaggageAreaOne = 200,
                BaggageAreaOneArm = 300,
                BaggageAreaTwo = 100,
                BaggageAreaTwoArm = 250,
                TotalFuel = 300,
                TotalFuelArm = 20,
                TaxiFuel = 30,
                TaxiFuelArm = 20,
                TripFuel = 90,
                TripFuelArm = 20
            };

            var service = _serviceProvider.GetService<IFlightPreparationService>();

            Assert.Catch<ArgumentException>(() => service.CalculateWeightBalance(weightBalanceModel), "Invalid data submitted.");
        }

        [Test]
        public void ZeroWeightMustThrow()
        {
            var weightBalanceModel = new WeightBalanceAddViewModel
            {
                BasicEmptyWeight = 0,
                BasicEmptyWeightArm = 40,
                FrontLeftSeat = 0,
                FrontLeftSeatArm = 37,
                FrontRightSeat = 0,
                FrontRightSeatArm = 37,
                RearLeftSeat = 0,
                RearLeftSeatArm = 67,
                RearRightSeat = 0,
                RearRightSeatArm = 67,
                BaggageAreaOne = 0,
                BaggageAreaOneArm = 80,
                BaggageAreaTwo = 0,
                BaggageAreaTwoArm = 100,
                TotalFuel = 0,
                TotalFuelArm = 47,
                TaxiFuel = 0,
                TaxiFuelArm = 47,
                TripFuel = 0,
                TripFuelArm = 47
            };

            var service = _serviceProvider.GetService<IFlightPreparationService>();

            Assert.Catch<ArgumentException>(() => service.CalculateWeightBalance(weightBalanceModel), "Cannot divide by zero.");
        }

        [Test]
        public void ValidWeightBalanceAddViewModelMustNotThrow()
        {
            var weightBalanceModel = new WeightBalanceAddViewModel
            {
                BasicEmptyWeight = 1492,
                BasicEmptyWeightArm = 39.2,
                FrontLeftSeat = 180,
                FrontLeftSeatArm = 37,
                FrontRightSeat = 180,
                FrontRightSeatArm = 37,
                RearLeftSeat = 0,
                RearLeftSeatArm = 73,
                RearRightSeat = 0,
                RearRightSeatArm = 73,
                BaggageAreaOne = 10,
                BaggageAreaOneArm = 95,
                BaggageAreaTwo = 20,
                BaggageAreaTwoArm = 123,
                TotalFuel = 43,
                TotalFuelArm = 47.9,
                TaxiFuel = 1,
                TaxiFuelArm = 47.9,
                TripFuel = 20,
                TripFuelArm = 47.9
            };

            CalculatedWeightBalanceViewModel expected = new CalculatedWeightBalanceViewModel
            {
                ZeroFuelWeight = 1882,
                ZeroFuelWeightArm = 39.97,
                ZeroFuelWeightMoment = 75216.4,
                RampWeight = 2140,
                RampWeightArm = 40.92,
                RampWeightMoment = 87574.6,
                TakeoffWeight = 2134,
                TakeoffWeightArm = 40.9,
                TakeoffWeightMoment = 87287.2,
                LandingWeight = 2014,
                LandingWeightArm = 40.49,
                LandingWeightMoment = 81539.2
            };

            var service = _serviceProvider.GetService<IFlightPreparationService>();

            var actual = service.CalculateWeightBalance(weightBalanceModel);

            Assert.DoesNotThrow(() => service.CalculateWeightBalance(weightBalanceModel));

            var expectedJson = JsonSerializer.Serialize(expected);
            var actualJson = JsonSerializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }

    }
}
