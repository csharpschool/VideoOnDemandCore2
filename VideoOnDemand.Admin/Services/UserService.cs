using Microsoft.AspNetCore.Identity;
using VideoOnDemand.Data.Data;
using VideoOnDemand.Data.Data.Entities;

namespace VideoOnDemand.Admin.Services
{
    public class UserService : IUserService
    {
        private VODContext _db;
        private readonly UserManager<User> _userManager;
        public UserService(VODContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
    }
}
