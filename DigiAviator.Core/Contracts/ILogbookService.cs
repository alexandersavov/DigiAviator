using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
    public interface ILogbookService
    {
        Task<LogbookViewModel> GetLogbook(string userId);
        Task<bool> HasLogbook(string userId);
        Task AddLogbook(string userId, LogbookAddViewModel model);
        Task AddFlightToLogbook(string id, FlightAddViewModel model);
        Task DeleteFlight(string languageId);
    }
}
