using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models.User;
using DigiAviator.Infrastructure.Data.Models.Identity;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigiAviator.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository _repo;

        public UserService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await _repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await _repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await _repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
