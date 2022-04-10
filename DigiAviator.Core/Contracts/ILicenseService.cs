using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
    public interface ILicenseService
    {
        Task<LicenseViewModel> GetLicense(string userId);
        Task<bool> HasLicense(string userId);
        Task<bool> AddLicense(string userId, LicenseAddViewModel model);
        Task<bool> AddLanguageToLicense(string id, LanguageAddViewModel model);
        Task<bool> AddRatingToLicense(string id, RatingAddViewModel model);
        Task<bool> DeleteLanguage(string languageId);
        Task<bool> DeleteRating(string ratingId);
        Task<LicenseAddViewModel> GetLicenseForEdit(string licenseId);
        Task<bool> UpdateLicense(string userId, LicenseAddViewModel model);
    }
}
