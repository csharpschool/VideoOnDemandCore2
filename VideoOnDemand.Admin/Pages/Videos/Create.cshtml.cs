using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoOnDemand.Admin.Services;
using VideoOnDemand.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace VideoOnDemand.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private IUserService _userService;

        [BindProperty]
        public RegisterUserPageModel Input { get; set; } = new RegisterUserPageModel();

        // Used to send a message back to the Index Razor Page.
        [TempData]
        public string StatusMessage { get; set; }

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddUserAsync(Input);

                if (result.Succeeded)
                {
                    // Message sent back to the Index Razor Page.
                    StatusMessage = $"Created a new account for { Input.Email}.";
    
                return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,
                        error.Description);
                }
            }

            // Something failed, redisplay the form.
            return Page();
        }
    }
}