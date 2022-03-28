using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
    public interface IMedicalService
    {
        Task<MedicalViewModel> GetMedical(string userId);
        Task<bool> AddMedical(string userId, MedicalAddViewModel model);
    }
}
