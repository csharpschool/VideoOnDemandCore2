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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetVideos(string userId, int moduleId = 0)
        {
            throw new NotImplementedException();
        }
    }
}
