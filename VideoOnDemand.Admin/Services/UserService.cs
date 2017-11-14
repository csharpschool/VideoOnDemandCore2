using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using VideoOnDemand.Admin.Models;
using VideoOnDemand.Data.Data;
using VideoOnDemand.Data.Data.Entities;
using System.Threading.Tasks;

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

        public async Task<IdentityResult> AddUserAsync(RegisterUserPageModel user)
        {
            var dbUser = new User
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(dbUser, user.Password);
            return result;
        }

        public UserPageModel GetUser(string userId)
        {
            return (from user in _db.Users
                    where user.Id.Equals(userId)
                    select new UserPageModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        IsAdmin = _db.UserRoles.Any(ur =>
                            ur.UserId.Equals(user.Id) &&
                            ur.RoleId.Equals(1.ToString()))
                    }).FirstOrDefault();
        }

        public IEnumerable<UserPageModel> GetUsers()
        {
            return from user in _db.Users
                   orderby user.Email
                   select new UserPageModel
                   {
                       Id = user.Id,
                       Email = user.Email,
                       IsAdmin = _db.UserRoles.Any(ur =>
                           ur.UserId.Equals(user.Id) &&
                           ur.RoleId.Equals(1.ToString()))
                   };
        }
    }
}
