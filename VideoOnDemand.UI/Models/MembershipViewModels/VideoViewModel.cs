using VideoOnDemand.UI.Models.DTOModels;

namespace VideoOnDemand.UI.Models.MembershipViewModels
{
    public class VideoViewModel
    {
        public VideoDTO Video { get; set; }
        public InstructorDTO Instructor { get; set; }
        public CourseDTO Course { get; set; }
        public LessonInfoDTO LessonInfo { get; set; }
    }
}
