using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoOnDemand.Data.Services;
using VideoOnDemand.Data.Data.Entities;
using VideoOnDemand.Admin.Services;
using VideoOnDemand.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace VideoOnDemand.Admin.Pages.UserCourses
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IDbWriteService _dbWriteService;
        private IDbReadService _dbReadService;
        private IUserService _userService;

        [BindProperty]
        public UserCoursePageModel Input { get; set; } = new UserCoursePageModel();

        [TempData]
        public string StatusMessage { get; set; } // Used to send a message back to the Index view
        
        public DeleteModel(IDbReadService dbReadService, IDbWriteService dbWriteService, IUserService userService)
        {
            _dbWriteService = dbWriteService;
            _dbReadService = dbReadService;
            _userService = userService;
        }

        public void OnGet(int courseId, string userId)
        {
            var user = _userService.GetUser(userId);
            var course = _dbReadService.Get<Course>(courseId);
            Input.UserCourse = _dbReadService.Get<UserCourse>(userId, courseId);
            Input.Email = user.Email;
            Input.CourseTitle = course.Title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var success = await _dbWriteService.Delete(Input.UserCourse);

                if (success)
                {
                    StatusMessage = $"User-Course combination [{Input.CourseTitle} | {Input.Email}] was deleted.";
                    return RedirectToPage("Index");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}