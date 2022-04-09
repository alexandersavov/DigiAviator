using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
    public interface ILogbookService
    {
        Task<LogbookViewModel> GetLogbook(string userId);
        Task<bool> AddLogbook(string userId, LogbookAddViewModel model);
        Task<bool> AddFlightToLogbook(string id, FlightAddViewModel model);
        Task<bool> DeleteFlight(string languageId);
    }
}
