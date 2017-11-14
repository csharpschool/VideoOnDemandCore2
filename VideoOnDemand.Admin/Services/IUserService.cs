using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoOnDemand.Admin.Models;

namespace VideoOnDemand.Admin.Services
{
    public interface IUserService
    {
        IEnumerable<UserPageModel> GetUsers();
        UserPageModel GetUser(string userId);
        Task<IdentityResult> AddUserAsync(RegisterUserPageModel user);
        Task<bool> UpdateUserAsync(UserPageModel user);
    }
}
