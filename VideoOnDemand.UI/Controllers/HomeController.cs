using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoOnDemand.UI.Models;
using Microsoft.AspNetCore.Identity;
using VideoOnDemand.Data.Data.Entities;
using VideoOnDemand.UI.Repositories;

namespace VideoOnDemand.UI.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<User> _signInManager;

        public HomeController(SignInManager<User> signInMgr)
        {
            _signInManager = signInMgr;
        }

        public IActionResult Index()
        {
            var rep = new MockReadRepository();
            var courses = rep.GetCourses("4ad684f8-bb70-4968-85f8-458aa7dc19a3");

            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
