using VideoOnDemand.Data.Data.Entities;

namespace VideoOnDemand.Admin.Models
{
    public class UserCoursePageModel
    {
        public string Email { get; set; }
        public string CourseTitle { get; set; }
        public UserCourse UserCourse { get; set; } = new UserCourse();
        public UserCourse UpdatedUserCourse { get; set; } = new UserCourse();
    }
}
