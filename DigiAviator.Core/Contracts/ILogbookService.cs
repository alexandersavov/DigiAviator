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
        Task<string> GetTotalFlightTime(string userId);
        Task<string> GetLongestFlight(string userId);
        Task<string> GetMostFlownAircraft(string userId);
        Task<LogbookAddViewModel> GetLogbookForEdit(string logbookId);
        Task<bool> UpdateLogbook(string userId, LogbookAddViewModel model);
    }
}
