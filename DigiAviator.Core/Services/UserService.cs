using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models.User;
using DigiAviator.Infrastructure.Data.Models.Identity;

namespace DigiAviator.Core.Services
{
    public class UserService : IUserService
    {
        public Task<ApplicationUser> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserEditViewModel> GetUserForEdit(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UserEditViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
