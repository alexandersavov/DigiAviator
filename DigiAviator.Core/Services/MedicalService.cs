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

        public async Task<bool> DeleteFitness(string fitnessTypeId)
        {
            bool isDeleted = false;
            
            try
            {
                await _repo.DeleteAsync<FitnessType>(Guid.Parse(fitnessTypeId));
                await _repo.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception)
            {

                throw;
            }
            
            return isDeleted;
        }

        public async Task<bool> DeleteLimitation(string limitationId)
        {
            bool isDeleted = false;

            try
            {
                await _repo.DeleteAsync<Limitation>(Guid.Parse(limitationId));
                await _repo.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception)
            {

                throw;
            }

            return isDeleted;
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
                    Id = limitation.Id.ToString(),
                    LimitationCode = limitation.LimitationCode,
                    Description = limitation.Description
                });
            };

            foreach (var fitnessType in medical.FitnessTypes)
            {
                fitnessTypes.Add(new FitnessTypeListViewModel
                {
                    Id = fitnessType.Id.ToString(),
                    FitnessClass = fitnessType.FitnessClass,
                    ValidUntil = fitnessType.ValidUntil.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
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

        public async Task<MedicalAddViewModel> GetMedicalForEdit(string medicalId)
        {
            var medical = await _repo.All<Medical>()
           .Where(m => m.Id == Guid.Parse(medicalId))
           .FirstOrDefaultAsync();

            return new MedicalAddViewModel()
            {
                IssuingAuthorithy = medical.IssuingAuthorithy,
                LicenseNumber = medical.LicenseNumber,
                MedicalNumber = medical.MedicalNumber,
                IssuedOn = medical.IssuedOn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                FirstName = medical.FirstName,
                MiddleName = medical.MiddleName,
                LastName = medical.LastName,
                BirthDate = medical.BirthDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                Nationality = medical.Nationality,
            };
        }

		public async Task<bool> HasMedical(string userId)
		{
			bool hasMedical = false;

            var medical = await _repo.All<Medical>()
                .Where(m => m.HolderId == userId)
                .FirstOrDefaultAsync();

            if (medical != null)
			{
                hasMedical = true;
			}

            return hasMedical;
        }

		public async Task<bool> UpdateMedical(string userId, MedicalAddViewModel model)
        {
            bool updated = false;

            DateTime.TryParse(model.BirthDate, out DateTime birthDate);
            DateTime.TryParse(model.IssuedOn, out DateTime issuedOnDate);

            var medical = await _repo.All<Medical>()
            .Where(m => m.HolderId == userId)
            .FirstOrDefaultAsync();

            if (medical != null)
            {
                medical.IssuingAuthorithy = model.IssuingAuthorithy;
                medical.LicenseNumber = model.LicenseNumber;
                medical.MedicalNumber = model.MedicalNumber;
                medical.FirstName = model.FirstName;
                medical.MiddleName = model.MiddleName;
                medical.LastName = model.LastName;
                medical.Nationality = model.Nationality;
                medical.BirthDate = birthDate;
                medical.IssuedOn = issuedOnDate;

                _repo.Update(medical);
                await _repo.SaveChangesAsync();
                updated = true;
            }

            return updated;
        }


    }
}
