using System.Collections.Generic;
using VideoOnDemand.Admin.Models;

namespace VideoOnDemand.Admin.Services
{
    public interface IUserService
    {
        IEnumerable<UserPageModel> GetUsers();
        UserPageModel GetUser(string userId);
    }
}
