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

        public async Task<bool> AddFlightToLogbook(string id, FlightAddViewModel model)
        {
            var logbook = await _repo.GetByIdAsync<Logbook>(Guid.Parse(id));

            bool result = false;

            var flight = new Flight
            {
                DateOfFlight = model.DateOfFlight,
                DepartureAirportICAO = model.DepartureAirportICAO,
                DepartureTimeUTC = model.DepartureTimeUTC,
                ArrivalAirportICAO = model.ArrivalAirportICAO,
                ArrivalTimeUTC = model.ArrivalTimeUTC,
                AircraftType = model.AircraftType,
                AircraftRegistration = model.AircraftRegistration,
                TotalFlightTime = model.TotalFlightTime,
                PilotInCommandName = model.PilotInCommandName,
                LandingsDay = model.LandingsDay,
                LandingsNight = model.LandingsNight,
                PilotInCommandFunctionTime = model.PilotInCommandFunctionTime,
                CopilotFunctionTime = model.CopilotFunctionTime,
                DualFunctionTime = model.DualFunctionTime,
                InstructorFunctionTime = model.InstructorFunctionTime,
                LogbookId = logbook.Id
            };

            if (logbook != null)
            {
                logbook.Flights.Add(flight);
                await _repo.AddAsync(flight);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> AddLogbook(string userId, LogbookAddViewModel model)
        {
            bool result = false;

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
                result = true;
            }

            return result;
        }

        public async Task<bool> DeleteFlight(string flightId)
        {
            bool isDeleted = false;

            try
            {
                await _repo.DeleteAsync<Flight>(Guid.Parse(flightId));
                await _repo.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception)
            {

                throw;
            }

            return isDeleted;
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
                    DepartureTimeUTC = flight.DepartureTimeUTC.ToString("hh:mm", CultureInfo.InvariantCulture),
                    ArrivalAirportICAO = flight.ArrivalAirportICAO,
                    ArrivalTimeUTC = flight.ArrivalTimeUTC.ToString("hh:mm", CultureInfo.InvariantCulture),
                    TotalFlightTime = flight.TotalFlightTime.ToString("hh:mm", CultureInfo.InvariantCulture),
                    PilotInCommandName = flight.PilotInCommandName,
                    LandingsDay = flight.LandingsDay,
                    LandingsNight = flight.LandingsNight,
                    PilotInCommandFunctionTime = flight.PilotInCommandFunctionTime.ToString("hh:mm", CultureInfo.InvariantCulture),
                    CopilotFunctionTime = flight.CopilotFunctionTime.ToString("hh:mm", CultureInfo.InvariantCulture),
                    DualFunctionTime = flight.DualFunctionTime.ToString("hh:mm", CultureInfo.InvariantCulture),
                    InstructorFunctionTime = flight.InstructorFunctionTime.ToString("hh:mm", CultureInfo.InvariantCulture),
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
    }
}