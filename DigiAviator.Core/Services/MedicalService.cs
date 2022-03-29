using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DigiAviator.Core.Services
{
    public class MedicalService : IMedicalService
    {
        private readonly IApplicationDbRepository _repo;

        public MedicalService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddFitnessToMedical(string id, FitnessTypeAddViewModel model)
        {
            var medical = await _repo.GetByIdAsync<Medical>(Guid.Parse(id));

            bool result = false;

            DateTime.TryParse(model.ValidUntil, out DateTime validUntilDate);

            var fitnessType = new FitnessType
            {
                MedicalId = medical.Id,
                FitnessClass = model.FitnessClass,
                ValidUntil = validUntilDate
            };

            medical.FitnessTypes.Add(fitnessType);

            if (medical != null)
            {
                await _repo.AddAsync(fitnessType);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> AddLimitationToMedical(string id, LimitationAddViewModel model)
        {
            var medical = await _repo.GetByIdAsync<Medical>(Guid.Parse(id));

            bool result = false;

            var limitation = new Limitation
            {
                MedicalId = medical.Id,
                LimitationCode = model.LimitationCode,
                Description = model.Description
            };

            medical.Limitations.Add(limitation);

            if (medical != null)
            {
                await _repo.AddAsync(limitation);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> AddMedical(string userId, MedicalAddViewModel model)
        {
            bool result = false;

            DateTime.TryParse(model.BirthDate, out DateTime birthDate);
            DateTime.TryParse(model.IssuedOn, out DateTime issuedOnDate);

            var medical = new Medical
            {
                IssuingAuthorithy = model.IssuingAuthorithy,
                LicenseNumber = model.LicenseNumber,
                MedicalNumber = model.MedicalNumber,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Nationality = model.Nationality,
                BirthDate = birthDate,
                IssuedOn = issuedOnDate,
                HolderId = userId
            };

            if (medical != null)
            {
                await _repo.AddAsync(medical);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<MedicalViewModel> GetMedical(string userId)
        {
            var medical = await _repo.All<Medical>()
           .Where(m => m.HolderId == userId)
           .Include(l => l.Limitations)
           .Include(f => f.FitnessTypes)
           .FirstOrDefaultAsync();

            List<LimitationListViewModel> limitations = new List<LimitationListViewModel>();
            List<FitnessTypeListViewModel> fitnessTypes = new List<FitnessTypeListViewModel>();

            foreach (var limitation in medical.Limitations)
            {
                limitations.Add(new LimitationListViewModel
                {
                    LimitationCode = limitation.LimitationCode,
                    Description = limitation.Description
                });
            };

            foreach (var fitnessType in medical.FitnessTypes)
            {
                fitnessTypes.Add(new FitnessTypeListViewModel
                {
                    FitnessClass = fitnessType.FitnessClass,
                    ValidUntil = fitnessType.ValidUntil.ToString(),
                });
            };

            return new MedicalViewModel()
            {
                Id = medical.Id.ToString(),
                IssuingAuthorithy = medical.IssuingAuthorithy,
                LicenseNumber = medical.LicenseNumber,
                MedicalNumber = medical.MedicalNumber,
                IssuedOn = medical.IssuedOn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                FirstName = medical.FirstName,
                MiddleName = medical.MiddleName,
                LastName = medical.LastName,
                BirthDate = medical.BirthDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                Nationality = medical.Nationality,
                HolderId = medical.HolderId,
                Limitations = limitations,
                FitnessTypes = fitnessTypes
            };
        }
    }
}
