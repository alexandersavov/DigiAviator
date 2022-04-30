using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DigiAviator.Core.Services
{
    public class LogbookService : ILogbookService
    {
        private readonly IApplicationDbRepository _repo;

        public LogbookService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task AddFlightToLogbook(string id, FlightAddViewModel model)
        {
            var logbook = await _repo.GetByIdAsync<Logbook>(Guid.Parse(id));

            DateTime.TryParse(model.DateOfFlight, out DateTime dateOfFlight);

            var flight = new Flight
            {
                DateOfFlight = dateOfFlight,
                DepartureAirportICAO = model.DepartureAirportICAO,
                DepartureTimeUTC = TimeSpan.Parse(model.DepartureTimeUTC),
                ArrivalAirportICAO = model.ArrivalAirportICAO,
                ArrivalTimeUTC = TimeSpan.Parse(model.ArrivalTimeUTC),
                AircraftType = model.AircraftType,
                AircraftRegistration = model.AircraftRegistration,
                TotalFlightTime = TimeSpan.Parse(model.TotalFlightTime),
                PilotInCommandName = model.PilotInCommandName,
                LandingsDay = model.LandingsDay,
                LandingsNight = model.LandingsNight,
                PilotInCommandFunctionTime = TimeSpan.Parse(model.PilotInCommandFunctionTime),
                CopilotFunctionTime = TimeSpan.Parse(model.CopilotFunctionTime),
                DualFunctionTime = TimeSpan.Parse(model.DualFunctionTime),
                InstructorFunctionTime = TimeSpan.Parse(model.InstructorFunctionTime),
                LogbookId = logbook.Id
            };

            if (logbook != null)
            {
                logbook.Flights.Add(flight);
                await _repo.AddAsync(flight);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task AddLogbook(string userId, LogbookAddViewModel model)
        {
            var logbook = new Logbook
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Address = model.Address,
                HolderId = userId
            };

            if (logbook != null)
            {
                await _repo.AddAsync(logbook);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task DeleteFlight(string flightId)
        {
            await _repo.DeleteAsync<Flight>(Guid.Parse(flightId));
            await _repo.SaveChangesAsync();
        }

        public async Task<LogbookViewModel> GetLogbook(string userId)
        {
            var logbook = await _repo.All<Logbook>()
                .Where(l => l.HolderId == userId)
                .Include(l => l.Flights)
                .FirstOrDefaultAsync();

            List<FlightListViewModel> flights = new List<FlightListViewModel>();

            foreach (var flight in logbook.Flights)
            {
                flights.Add(new FlightListViewModel
                {
                    Id = flight.Id.ToString(),
                    DateOfFlight = flight.DateOfFlight.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                    DepartureAirportICAO = flight.DepartureAirportICAO,
                    DepartureTimeUTC = flight.DepartureTimeUTC.ToString(@"hh\:mm"),
                    ArrivalAirportICAO = flight.ArrivalAirportICAO,
                    ArrivalTimeUTC = flight.ArrivalTimeUTC.ToString(@"hh\:mm"),
                    AircraftType = flight.AircraftType,
                    AircraftRegistration = flight.AircraftRegistration,
                    TotalFlightTime = flight.TotalFlightTime.ToString(@"hh\:mm"),
                    PilotInCommandName = flight.PilotInCommandName,
                    LandingsDay = flight.LandingsDay,
                    LandingsNight = flight.LandingsNight,
                    PilotInCommandFunctionTime = flight.PilotInCommandFunctionTime.ToString(@"hh\:mm"),
                    CopilotFunctionTime = flight.CopilotFunctionTime.ToString(@"hh\:mm"),
                    DualFunctionTime = flight.DualFunctionTime.ToString(@"hh\:mm"),
                    InstructorFunctionTime = flight.InstructorFunctionTime.ToString(@"hh\:mm"),
                });
            };

            return new LogbookViewModel()
            {
                Id = logbook.Id.ToString(),
                FirstName = logbook.FirstName,
                MiddleName = logbook.MiddleName,
                LastName = logbook.LastName,
                Address = logbook.Address,
                Flights = flights,
                HolderId = logbook.HolderId
            };
        }

        public async Task<LogbookAddViewModel> GetLogbookForEdit(string logbookId)
        {
            var logbook = await _repo.All<Logbook>()
                .Where(l => l.Id == Guid.Parse(logbookId))
                .FirstOrDefaultAsync();

            return new LogbookAddViewModel()
            {
                FirstName = logbook.FirstName,
                MiddleName= logbook.MiddleName,
                LastName= logbook.LastName,
                Address= logbook.Address
            };
        }

        public async Task<string> GetLongestFlight(string userId)
        {
            var logbook = await _repo.All<Logbook>()
                .Where(l => l.HolderId == userId)
                .Include(l => l.Flights)
                .FirstOrDefaultAsync();

            if (logbook == null)
            {
                return "No logbook found. Longest flight time is 00:00";
            }

            string longestFlightTime = String.Empty;

            var longestFlight = logbook.Flights
                .OrderByDescending(f => f.TotalFlightTime)
                .FirstOrDefault();

            if (longestFlight == null)
            {
                longestFlightTime = "No flights logged!";
            }
            else
            {
                longestFlightTime = longestFlight.TotalFlightTime.ToString(@"hh\:mm");
            }

            return longestFlightTime;
        }



        public async Task<string> GetMostFlownAircraft(string userId)
        {
            var logbook = await _repo.All<Logbook>()
                .Where(l => l.HolderId == userId)
                .Include(l => l.Flights)
                .FirstOrDefaultAsync();

            if (logbook == null)
            {
                return "No logbook found.";
            }

            var mostFlownAircraft = logbook.Flights
                   .GroupBy(f => f.AircraftRegistration)
                   .Select(a => new { name = a.Key, count = a.Count() })
                   .First();

            string result = mostFlownAircraft.name;

            return result;
        }

        public async Task<string> GetTotalFlightTime(string userId)
        {
            var logbook = await _repo.All<Logbook>()
                .Where(l => l.HolderId == userId)
                .Include(l => l.Flights)
                .FirstOrDefaultAsync();

            if (logbook == null)
            {
                return "No logbook found. Flight time is 00:00";
            }

            TimeSpan totalTime = TimeSpan.Zero;

            foreach (var flight in logbook.Flights)
            {
                totalTime += flight.TotalFlightTime;
            };

            string totalFlightTime = String.Empty;

            if (totalTime == TimeSpan.MinValue)
            {
                totalFlightTime = "No flights logged!";
            }
            else
            {
                totalFlightTime = totalTime.ToString(@"hh\:mm");
            }

            return totalFlightTime;
        }

        public async Task<bool> HasLogbook(string userId)
		{
            bool hasMedical = false;

            var logbook = await _repo.All<Logbook>()
                .Where(m => m.HolderId == userId)
                .FirstOrDefaultAsync();

            if (logbook != null)
            {
                hasMedical = true;
            }

            return hasMedical;
        }

        public async Task<bool> UpdateLogbook(string userId, LogbookAddViewModel model)
        {
            bool updated = false;

            var logbook = await _repo.All<Logbook>()
                .Where(m => m.HolderId == userId)
                .FirstOrDefaultAsync();

            if (logbook != null)
            {
                logbook.FirstName = model.FirstName;
                logbook.MiddleName = model.MiddleName;
                logbook.LastName = model.LastName;
                logbook.Address = model.Address;

                _repo.Update(logbook);
                await _repo.SaveChangesAsync();
                updated = true;
            }

            return updated;
        }
    }
}