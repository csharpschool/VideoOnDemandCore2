using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoOnDemand.UI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using VideoOnDemand.Data.Data.Entities;
using AutoMapper;
using VideoOnDemand.UI.Models.DTOModels;
using VideoOnDemand.UI.Models.MembershipViewModels;

namespace VideoOnDemand.UI.Controllers
{
    public class MembershipController : Controller
    {
        private string _userId;
        private IReadRepository _db;
        private readonly IMapper _mapper;
        public MembershipController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IMapper mapper, IReadRepository db)
        {
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var courseDtoObjects = _mapper.Map<List<CourseDTO>>(_db.GetCourses(_userId));

            var dashboardModel = new DashboardViewModel();
            dashboardModel.Courses = new List<List<CourseDTO>>();

            var noOfRows = courseDtoObjects.Count <= 3 ? 1 :
                courseDtoObjects.Count / 3;
            for (var i = 0; i < noOfRows; i++)
            {
                dashboardModel.Courses.Add(courseDtoObjects.Take(3).ToList());
            }

            return View(dashboardModel);
        }

        [HttpGet]
        public IActionResult Course(int id)
        {
            var course = _db.GetCourse(_userId, id);
            var mappedCourseDTOs = _mapper.Map<CourseDTO>(course);
            var mappedInstructorDTO = _mapper.Map<InstructorDTO>(course.Instructor);
            var mappedModuleDTOs = _mapper.Map<List<ModuleDTO>>(course.Modules);

            for (var i = 0; i < mappedModuleDTOs.Count; i++)
            {
                mappedModuleDTOs[i].Downloads =
                    course.Modules[i].Downloads.Count.Equals(0) ? null :
                    _mapper.Map<List<DownloadDTO>>(course.Modules[i].Downloads);

                mappedModuleDTOs[i].Videos =
                    course.Modules[i].Videos.Count.Equals(0) ? null :
                    _mapper.Map<List<VideoDTO>>(course.Modules[i].Videos);
            }

            var courseModel = new CourseViewModel
            {
                Course = mappedCourseDTOs,
                Instructor = mappedInstructorDTO,
                Modules = mappedModuleDTOs
            };

            return View(courseModel);
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            return View();
        }
    }
}