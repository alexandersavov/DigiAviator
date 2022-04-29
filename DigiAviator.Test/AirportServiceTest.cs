using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Core.Services;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigiAviator.Test
{
    public class AirportServiceTest
    {
        private ServiceProvider _serviceProvider;
        private InMemoryDbContext _dbContext;

        [SetUp]
        public async Task Setup()
        {
            _dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            _serviceProvider = serviceCollection
                .AddSingleton(sp => _dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IAirportService, AirportService>()
                .AddSingleton<IValidationService, ValidationService>()
                .BuildServiceProvider();

            var repo = _serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAirportsMustNotThrow()
        {
            var service = _serviceProvider.GetService<IAirportService>();

            List<AirportListViewModel> expected = new List<AirportListViewModel>();

            expected.Add(new AirportListViewModel
            {
                Id = "3c1024ce-b2b9-4b0f-928e-ebe8b839ddb8".ToUpper(),
                Name = "Lesnovo Airport",
                IcaoIdentifier = "LBLS",
                Elevation = 1824
            });

            var actual = (List<AirportListViewModel>) await service.GetAirports();

            Assert.DoesNotThrowAsync(async () => await service.GetAirports());
            var expectedJson = JsonSerializer.Serialize(expected);
            var actualJson = JsonSerializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void WrongAirportIdMustThrow()
        {
            var airportId = Guid.NewGuid();

            var service = _serviceProvider.GetService<IAirportService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetAirportById(airportId), "Unknown airport id");
        }

        [Test]
        public async Task ExisitingAirportIdMustNotThrow()
        {
            var airportId = Guid.Parse("3c1024ce-b2b9-4b0f-928e-ebe8b839ddb8");

            var expected = new Airport
            {
                Id = airportId,
                Name = "Lesnovo Airport",
                IcaoIdentifier = "LBLS",
                Description = "Test description",
                Type = "General aviation airport",
                Latitude = 24.5463643,
                Longitude = 13.324235,
                Elevation = 1824,
                Runways = new List<Runway>
                {
                    new Runway
                    {
                        Id = Guid.Parse("1773aedd-4a9e-4625-9d71-b4293a716f76"),
                        Designation = "09",
                        TrueCourse = "090",
                        MagneticCourse = "089",
                        Length = 3000,
                        Width = 50,
                        Slope = 0,
                        TORA = 2000,
                        TODA = 2000,
                        ASDA = 2000,
                        LDA = 2000,
                        AirportId = airportId
                    }
                }
            };

            var service = _serviceProvider.GetService<IAirportService>();

            var actual = await service.GetAirportById(airportId);

            Assert.DoesNotThrowAsync(async () => await service.GetAirportById(airportId));
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IcaoIdentifier, actual.IcaoIdentifier);
            Assert.AreEqual(expected.Runways.Count, actual.Runways.Count);

            for (int i = 0; i < actual.Runways.Count; i++)
            {
                Assert.AreEqual(expected.Runways[i].Id, actual.Runways[i].Id);
            }
        }

        [Test]
        public void WrongAirportDetailsIdMustThrow()
        {
            var airportId = Guid.NewGuid().ToString();

            var service = _serviceProvider.GetService<IAirportService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetAirportDetails(airportId), "Unknown airport id");
        }

        [Test]
        public async Task ExisitingAirportIdDetailsMustNotThrow()
        {
            var airportId = "3c1024ce-b2b9-4b0f-928e-ebe8b839ddb8";

            var expected = new AirportDetailsViewModel
            {
                Id = airportId,
                Name = "Lesnovo Airport",
                IcaoIdentifier = "LBLS",
                Description = "Test description",
                Type = "General aviation airport",
                Latitude = 24.5463643,
                Longitude = 13.324235,
                Elevation = 1824,
                Runways = new List<RunwayListViewModel>
                {
                    new RunwayListViewModel
                    {
                        Designation = "09",
                        TrueCourse = "090",
                        MagneticCourse = "089",
                        Length = 3000,
                        Width = 50,
                        Slope = 0,
                        TORA = 2000,
                        TODA = 2000,
                        ASDA = 2000,
                        LDA = 2000
                    }
                }
            };

            var service = _serviceProvider.GetService<IAirportService>();

            var actual = await service.GetAirportDetails(airportId);

            Assert.DoesNotThrowAsync(async () => await service.GetAirportDetails(airportId));
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IcaoIdentifier, actual.IcaoIdentifier);
            Assert.AreEqual(expected.Runways.Count, actual.Runways.Count);
        }

        [Test]
        public void InvalidAddAirportModelMustThrow()
        {
            var airport = new AirportAddViewModel
            {
                Name = "TooLongInvalidNameForTestingPurposesToThrowErrorMessage",
                IcaoIdentifier = "InvalidIdentifier",
                Type = "InvalidAirportTypeForTestingPurposes",
                Latitude = 190,
                Longitude = 100,
                Elevation = 200
            };

            var service = _serviceProvider.GetService<IAirportService>();

            Assert.CatchAsync<Exception>(async () => await service.AddAirport(airport), "Invalid airport information.");
        }

        [Test]
        public void WrongAddRunwayAirportIdMustThrow()
        {
            var airportId = Guid.NewGuid().ToString();

            var runway = new RunwayAddViewModel()
            {
                Designation = "09",
                TrueCourse = "090",
                MagneticCourse = "089",
                Length = 3000,
                Width = 50,
                Slope = 0,
                TORA = 2000,
                TODA = 2000,
                ASDA = 2000,
                LDA = 2000
            };

            var service = _serviceProvider.GetService<IAirportService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.AddRunwayToAirport(airportId, runway), "Unknown airport id.");
        }

        [Test]
        public void InvalidAddRunwayModelMustThrow()
        {
            var airportId = "3c1024ce-b2b9-4b0f-928e-ebe8b839ddb8";

            var runway = new RunwayAddViewModel
            {
                Designation = "ThisIsAnInvalidDesignationTooLong",
                TrueCourse = "TooLong",
                MagneticCourse = "VeryLong",
                Length = -1,
                Width = -1,
                Slope = 0,
                TODA = -1,
                TORA = -1,
                ASDA = -1,
                LDA = -1
            };

            var service = _serviceProvider.GetService<IAirportService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.AddRunwayToAirport(airportId, runway),"Invalid runway information, could not add runway to airport");
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var airport = new Airport()
            {
                Id = Guid.Parse("3c1024ce-b2b9-4b0f-928e-ebe8b839ddb8"),
                Name = "Lesnovo Airport",
                IcaoIdentifier = "LBLS",
                Description = "Test description",
                Type = "General aviation airport",
                Latitude = 24.5463643,
                Longitude = 13.324235,
                Elevation = 1824
            };

            var runway = new Runway()
            {
                Id= Guid.Parse("1773aedd-4a9e-4625-9d71-b4293a716f76"),
                Designation = "09",
                TrueCourse = "090",
                MagneticCourse = "089",
                Length = 3000,
                Width = 50,
                Slope = 0,
                TORA = 2000,
                TODA = 2000,
                ASDA = 2000,
                LDA = 2000,
                Airport = airport
            };

            airport.Runways.Add(runway);

            await repo.AddAsync(airport);
            await repo.AddAsync(runway);
            await repo.SaveChangesAsync();
        }
    }
}