using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
    public interface IMedicalService
    {
        Task<MedicalViewModel> GetMedical(string userId);
        Task<bool> AddMedical(string userId, MedicalAddViewModel model);
        Task<bool> AddLimitationToMedical(string id, LimitationAddViewModel model);
        Task<bool> AddFitnessToMedical(string id, FitnessTypeAddViewModel model);
    }
}
