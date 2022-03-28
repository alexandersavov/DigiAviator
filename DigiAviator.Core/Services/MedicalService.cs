using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigiAviator.Core.Services
{
    public class MedicalService : IMedicalService
    {
        private readonly IApplicationDbRepository _repo;

        public MedicalService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddMedical(string userId, MedicalAddViewModel model)
        {
            bool result = false;

            var medical = new Medical
            {
                IssuingAuthorithy = model.IssuingAuthorithy,
                LicenseNumber = model.LicenseNumber,
                MedicalNumber = model.MedicalNumber,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Nationality = model.Nationality,
                BirthDate = model.BirthDate,
                IssuedOn = model.IssuedOn,
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
           .FirstOrDefaultAsync();

            return new MedicalViewModel()
            {
                Id = medical.Id.ToString(),
                IssuingAuthorithy = medical.IssuingAuthorithy,
                LicenseNumber = medical.LicenseNumber,
                MedicalNumber = medical.MedicalNumber,
                IssuedOn = medical.IssuedOn,
                FirstName = medical.FirstName,
                MiddleName = medical.MiddleName,
                LastName = medical.LastName,
                BirthDate = medical.BirthDate,
                Nationality = medical.Nationality,
                HolderId = medical.HolderId
            };
        }
    }
}
