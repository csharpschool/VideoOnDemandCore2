using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemand.Data.Data.Entities;
using VideoOnDemand.Data.Services;

namespace VideoOnDemand.UI.Repositories
{
    public class SqlReadRepository : IReadRepository
    {
        private IDbReadService _db;

        public SqlReadRepository(IDbReadService db)
        {
            _db = db;
        }

        public Course GetCourse(string userId, int courseId)
        {
            var hasAccess = _db.Get<UserCourse>(userId, courseId) != null;
            if (!hasAccess) return default(Course);

            var course = _db.Get<Course>(courseId, true);

            foreach (var module in course.Modules)
            {
                module.Downloads = _db.Get<Download>().Where(d =>
                    d.ModuleId.Equals(module.Id)).ToList();

                module.Videos = _db.Get<Video>().Where(d =>
                    d.ModuleId.Equals(module.Id)).ToList();
            }

            return course;
        }

        public IEnumerable<Course> GetCourses(string userId)
        {
            var courses = _db.GetWithIncludes<UserCourse>()
                .Where(uc => uc.UserId.Equals(userId))
                .Select(c => c.Course);

            return courses;
        }

        public Video GetVideo(string userId, int videoId)
        {
            var video = _db.Get<Video>(videoId);

            var hasAccess = _db.Get<UserCourse>(userId, video.CourseId) != null;
            if (!hasAccess) return default(Video);

            return video;
        }

        public IEnumerable<Video> GetVideos(string userId, int moduleId = 0)
        {
            var module = _db.Get<Module>(moduleId);

            var hasAccess = _db.Get<UserCourse>(userId, module.CourseId) != null;
            if (!hasAccess) return default(IEnumerable<Video>);

            var videos = _db.Get<Video>().Where(v => v.ModuleId.Equals(moduleId));

            return videos;
        }
    }
}
