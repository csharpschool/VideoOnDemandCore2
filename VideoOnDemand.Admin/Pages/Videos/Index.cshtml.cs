using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using VideoOnDemand.Admin.Services;
using VideoOnDemand.Admin.Models;

namespace VideoOnDemand.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IEnumerable<UserPageModel> Users = new List<UserPageModel>();

        [TempData]
        public string StatusMessage { get; set; }


        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}