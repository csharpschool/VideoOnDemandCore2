using System.Collections.Generic;
using VideoOnDemand.UI.Models.DTOModels;

namespace VideoOnDemand.UI.Models.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<CourseDTO>> Courses { get; set; }
    }
}
