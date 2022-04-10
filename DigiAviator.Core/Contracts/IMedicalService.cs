using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
    public interface IMedicalService
    {
        Task<MedicalViewModel> GetMedical(string userId);
        Task<bool> HasMedical(string userId);
        Task<bool> AddMedical(string userId, MedicalAddViewModel model);
        Task<bool> AddLimitationToMedical(string id, LimitationAddViewModel model);
        Task<bool> AddFitnessToMedical(string id, FitnessTypeAddViewModel model);
        Task<bool> DeleteLimitation(string limitationId);
        Task<bool> DeleteFitness(string fitnessTypeId);
        Task<MedicalAddViewModel> GetMedicalForEdit(string medicalId);
        Task<bool> UpdateMedical(string userId, MedicalAddViewModel model);
    }
}
