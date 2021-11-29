using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Application.Model.Users
{
    public class UserFilterDto
    {
        public string Search { get; set; }
        public ICollection<int> StateIds { get; set; }
        public ICollection<int> RoleIds{ get; set; }
        public DateTime? PreviousPeDate { get; set; }
        public DateTime? NextPeDate { get; set; }
        public int? UserNameSort { get; set; }
        public int? UserPreviousPE { get; set; }
        public int? UserNextPE { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

    }
}
