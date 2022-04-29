using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace DigiAviator.Core.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly IApplicationDbRepository _repo;

        public LicenseService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }


        public async Task<bool> AddLanguageToLicense(string id, LanguageAddViewModel model)
        {
            var license = await _repo.GetByIdAsync<License>(Guid.Parse(id));

            bool result = false;

            DateTime.TryParse(model.ValidUntil, out DateTime validUntilDate);

            var language = new Language
            {
                LicenseId = license.Id,
                LanguageName = model.LanguageName,
                IcaoLevel = model.IcaoLevel,
                DateOfValidity = validUntilDate
            };            

            if (license != null)
            {
                license.LanguageProficiency.Add(language);
                await _repo.AddAsync(language);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> AddLicense(string userId, LicenseAddViewModel model)
        {
            bool result = false;

            DateTime.TryParse(model.BirthDate, out DateTime birthDate);
            DateTime.TryParse(model.DateOfInitialIssue, out DateTime issuedOnDate);

            var license = new License
            {
                IssueState = model.IssueState,
                LicenseNumber = model.LicenseNumber,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Address = model.Address,
                Nationality = model.Nationality,
                BirthDate = birthDate,
                IssuingAuthorithy = model.IssuingAuthorithy,
                TitleOfLicense = model.TitleOfLicense,
                DateOfInitialIssue = issuedOnDate,
                CountryCode = model.CountryCode,
                HolderId = userId
            };

            if (license != null)
            {
                await _repo.AddAsync(license);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> AddRatingToLicense(string id, RatingAddViewModel model)
        {
            var license = await _repo.GetByIdAsync<License>(Guid.Parse(id));

            bool result = false;

            DateTime.TryParse(model.ValidUntil, out DateTime validUntilDate);

            var rating = new Rating
            {
                ClassType = model.ClassType,
                Validity = validUntilDate
            };

            license.Ratings.Add(rating);

            if (license != null)
            {
                await _repo.AddAsync(rating);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> DeleteLanguage(string languageId)
        {
            bool isDeleted = false;

            try
            {
                await _repo.DeleteAsync<Language>(Guid.Parse(languageId));
                await _repo.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception)
            {

                throw;
            }

            return isDeleted;
        }

        public async Task<bool> DeleteRating(string ratingId)
        {
            bool isDeleted = false;

            try
            {
                await _repo.DeleteAsync<Rating>(Guid.Parse(ratingId));
                await _repo.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception)
            {

                throw;
            }

            return isDeleted;
        }

        public async Task<LicenseViewModel> GetLicense(string userId)
        {
            var license = await _repo.All<License>()
            .Where(m => m.HolderId == userId)
            .Include(l => l.LanguageProficiency)
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync();

            List<LanguageListViewModel> languages = new List<LanguageListViewModel>();
            List<RatingListViewModel> ratings = new List<RatingListViewModel>();

            foreach (var language in license.LanguageProficiency)
            {
                languages.Add(new LanguageListViewModel
                {
                    Id = language.Id.ToString(),
                    LanguageName = language.LanguageName,
                    IcaoLevel = language.IcaoLevel,
                    ValidUntil = language.DateOfValidity.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)
                });
            };

            foreach (var rating in license.Ratings)
            {
                ratings.Add(new RatingListViewModel
                {
                    Id = rating.Id.ToString(),
                    ClassType = rating.ClassType,
                    ValidUntil = rating.Validity.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)
                });
            };

            return new LicenseViewModel()
            {
                Id = license.Id.ToString(),
                IssueState = license.IssueState,
                IssuingAuthority = license.IssuingAuthorithy,
                LicenseNumber = license.LicenseNumber,
                FirstName = license.FirstName,
                MiddleName = license.MiddleName,
                LastName = license.LastName,
                Address = license.Address,
                Nationality = license.Nationality,
                CountryCode = license.CountryCode,
                DateOfInitialIssue = license.DateOfInitialIssue.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                TitleOfLicense = license.TitleOfLicense,
                Languages = languages,
                BirthDate = license.BirthDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                Ratings = ratings,
                HolderId = license.HolderId
            };
        }

        public async Task<LicenseAddViewModel> GetLicenseForEdit(string licenseId)
        {
            var license = await _repo.All<License>()
            .Where(l => l.Id == Guid.Parse(licenseId))
            .FirstOrDefaultAsync();

            return new LicenseAddViewModel()
            {
                IssueState = license.IssueState,
                LicenseNumber = license.LicenseNumber,
                FirstName = license.FirstName,
                MiddleName = license.MiddleName,
                LastName = license.LastName,
                Address = license.Address,
                Nationality = license.Nationality,
                BirthDate = license.BirthDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                IssuingAuthorithy = license.IssuingAuthorithy,
                TitleOfLicense = license.TitleOfLicense,
                DateOfInitialIssue = license.DateOfInitialIssue.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                CountryCode = license.CountryCode
            };
        }

		public async Task<bool> HasLicense(string userId)
		{
            bool hasLicense = false;

            var license = await _repo.All<License>()
                .Where(m => m.HolderId == userId)
                .FirstOrDefaultAsync();

            if (license != null)
            {
                hasLicense = true;
            }

            return hasLicense;
        }

        public async Task<string> HasValidLicense(string userId)
        {
            var license = await _repo.All<License>()
                .Where(l => l.HolderId == userId)
                .Include(l => l.Ratings)
                .FirstOrDefaultAsync();

            if (license == null)
            {
                return "No license added.";
            }

            if (license.Ratings.Count == 0)
            {
                return "Your license currently has no valid ratings.";
            }

            var sb = new StringBuilder();

            foreach (var rating in license.Ratings)
            {
                if (rating.Validity > DateTime.Now)
                {
                    sb.AppendLine($"{rating.ClassType} valid until {rating.Validity:D}");
                }
                else
                {
                    sb.AppendLine($"{rating.ClassType} expired on {rating.Validity:D}");
                }
            }

            return sb.ToString();
        }

        public async Task<bool> UpdateLicense(string userId, LicenseAddViewModel model)
        {
            bool updated = false;

            DateTime.TryParse(model.BirthDate, out DateTime birthDate);
            DateTime.TryParse(model.DateOfInitialIssue, out DateTime issuedOnDate);

            var license = await _repo.All<License>()
            .Where(l => l.HolderId == userId)
            .FirstOrDefaultAsync();

            if (license != null)
            {
                license.IssueState = model.IssueState;
                license.LicenseNumber = model.LicenseNumber;
                license.FirstName = model.FirstName;
                license.MiddleName = model.MiddleName;
                license.LastName = model.LastName;
                license.Address = model.Address;
                license.Nationality = model.Nationality;
                license.BirthDate = birthDate;
                license.IssuingAuthorithy = model.IssuingAuthorithy;
                license.TitleOfLicense = model.TitleOfLicense;
                license.DateOfInitialIssue = issuedOnDate;
                license.CountryCode = model.CountryCode;

                _repo.Update(license);
                await _repo.SaveChangesAsync();
                updated = true;
            }

            return updated;
        }
    }
}
