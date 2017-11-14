using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoOnDemand.Data.Data.Entities;
using VideoOnDemand.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace VideoOnDemand.Admin.Pages.Downloads
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IDbWriteService _dbWriteService;
        private IDbReadService _dbReadService;

        [BindProperty]
        public Download Input { get; set; } = new Download();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view

        public EditModel(IDbReadService dbReadService, IDbWriteService dbWriteService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
        }

        public void OnGet(int id)
        {
            ViewData["Modules"] = _dbReadService.GetSelectList<Module>("Id", "Title");
            Input = _dbReadService.Get<Download>(id, true);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Input.CourseId = _dbReadService.Get<Module>(Input.ModuleId).CourseId;
                Input.Course = null;
                var success = await _dbWriteService.Update(Input);

                if (success)
                {
                    StatusMessage = $"Updated Download: {Input.Title}.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}