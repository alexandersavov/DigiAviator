using DigiAviator.Core.Models.User;
using DigiAviator.Infrastructure.Data.Models.Identity;

namespace DigiAviator.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);
    }
}
