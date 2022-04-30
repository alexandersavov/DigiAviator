using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Core.Services;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Models.Identity;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigiAviator.Test
{
    public class LogbookServiceTest
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
                .AddSingleton<ILogbookService, LogbookService>()
                .AddSingleton<IValidationService, ValidationService>()
                .BuildServiceProvider();

            var repo = _serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetLogbookMustNotThrow()
        {
            var service = _serviceProvider.GetService<ILogbookService>();

            LogbookViewModel expected = new LogbookViewModel
            {
                Id = "b9eddc15-68dc-4a59-87b8-12e2586d3463",
                FirstName = "NameTest",
                MiddleName = "MiddleNameTest",
                LastName = "LastNameTest",
                Address = "Sofia, 1000, Test address",
                HolderId = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa6",
                Flights = new List<FlightListViewModel>
                {
                    new FlightListViewModel
                    {
                        Id = "1773aedd-4a9e-4625-9d71-b4293a716f76",
                        DateOfFlight = "09/5/2019",
                        DepartureAirportICAO = "LBSF",
                        DepartureTimeUTC = "10:00",
                        ArrivalAirportICAO = "LBLS",
                        ArrivalTimeUTC = "11:00",
                        AircraftType = "C-172",
                        AircraftRegistration = "LZ-GAS",
                        TotalFlightTime = "01:00",
                        PilotInCommandName = "SAVOV",
                        LandingsDay = 5,
                        LandingsNight = 0,
                        PilotInCommandFunctionTime = "01:00",
                        CopilotFunctionTime = "00:00",
                        DualFunctionTime = "00:00",
                        InstructorFunctionTime = "00:00"
                    }
                }
            };

            string holderId = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa6";

            var actual = await service.GetLogbook(holderId);

            Assert.DoesNotThrowAsync(async () => await service.GetLogbook(holderId));
            var expectedJson = JsonSerializer.Serialize(expected);
            var actualJson = JsonSerializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void WrongUserIdMustThrow()
        {
            var userId = Guid.NewGuid().ToString();

            var service = _serviceProvider.GetService<ILogbookService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetLogbook(userId), "Unknown user id.");
        }

        [Test]
        public void InvalidAddLogbookModelMustThrow()
        {
            string holderId = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa6";

            var logbook = new LogbookAddViewModel
            {
                 FirstName = String.Empty,
                 MiddleName = String.Empty,
                 LastName = String.Empty,
                 Address = String.Empty
            };

            var service = _serviceProvider.GetService<ILogbookService>();

            Assert.CatchAsync<Exception>(async () => await service.AddLogbook(holderId, logbook), "Invalid logbook information.");
        }

        [Test]
        public void ValidAddLogbookModelMustNotThrow()
        {
            string holderId = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa5";

            var logbook = new LogbookAddViewModel
            {
                FirstName = "NameTest",
                MiddleName = "MiddleNameTest",
                LastName = "LastNameTest",
                Address = "Sofia, 1000, Test address",
            };

            var service = _serviceProvider.GetService<ILogbookService>();

            Assert.DoesNotThrowAsync(async () => await service.AddLogbook(holderId, logbook));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var holderOne = new ApplicationUser()
            {
                Id = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa6"
            };

            var holderTwo = new ApplicationUser()
            {
                Id = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa5"
            };

            await repo.AddAsync(holderOne);
            await repo.AddAsync(holderTwo);

            var logbook = new Logbook()
            {
                Id = Guid.Parse("b9eddc15-68dc-4a59-87b8-12e2586d3463"),
                FirstName = "NameTest",
                MiddleName = "MiddleNameTest",
                LastName = "LastNameTest",
                Address = "Sofia, 1000, Test address",
                HolderId = "eb3a1025-6ebf-4639-8bbe-1decf56fdaa6"

            };

            await repo.AddAsync(logbook);

            var flight = new Flight()
            {
                Id = Guid.Parse("1773aedd-4a9e-4625-9d71-b4293a716f76"),
                DateOfFlight = new DateTime(2019, 05, 09),
                DepartureAirportICAO = "LBSF",
                DepartureTimeUTC = TimeSpan.FromHours(10),
                ArrivalAirportICAO= "LBLS",
                ArrivalTimeUTC = TimeSpan.FromHours(11),
                AircraftType = "C-172",
                AircraftRegistration = "LZ-GAS",
                TotalFlightTime = TimeSpan.FromHours(1),
                PilotInCommandName = "SAVOV",
                LandingsDay = 5,
                LandingsNight = 0,
                PilotInCommandFunctionTime = TimeSpan.FromHours(1),
                CopilotFunctionTime = TimeSpan.Zero,
                DualFunctionTime = TimeSpan.Zero,
                InstructorFunctionTime = TimeSpan.Zero,
                Logbook = logbook
            };


            await repo.AddAsync(flight);
            await repo.SaveChangesAsync();
        }
    }
}
