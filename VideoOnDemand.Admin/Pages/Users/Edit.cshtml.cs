using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using VideoOnDemand.Admin.Models;
using VideoOnDemand.Admin.Services;

namespace VideoOnDemand.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IUserService _userService;

        [BindProperty]
        public UserPageModel Input { get; set; } = new UserPageModel();

        [TempData]
        public string StatusMessage { get; set; }

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet(string userId)
        {
            Input = _userService.GetUser(userId);
            StatusMessage = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(Input);

                if (result)
                {
                    StatusMessage = $"User {Input.Email} was updated.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}