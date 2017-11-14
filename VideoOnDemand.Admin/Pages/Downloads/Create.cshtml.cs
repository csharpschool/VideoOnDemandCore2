using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoOnDemand.Data.Services;
using VideoOnDemand.Data.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace VideoOnDemand.Admin.Pages.Downloads
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private IDbReadService _dbReadService;
        private IDbWriteService _dbWriteService;

        [BindProperty]
        public Download Input { get; set; } = new Download();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public CreateModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbReadService = dbReadService;
            _dbWriteService = dbWriteService;
        }

        public void OnGet()
        {
            ViewData["Modules"] = _dbReadService.GetSelectList<Module>("Id", "Title");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Input.CourseId = _dbReadService.Get<Module>(Input.ModuleId).CourseId;
                var success = await _dbWriteService.Add(Input);

                if (success)
                {
                    StatusMessage = $"Created a new Download: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}