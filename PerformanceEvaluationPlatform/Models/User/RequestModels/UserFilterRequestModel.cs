using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class UserFilterRequestModel
    {
        public string EmailFilter { get; set; }
        public string FullNameFilter { get; set; }
        public int? StateIdFilter { get; set; }
        public int? RoleIdFilter { get; set; }
        public DateTime? PreviousPEDateFilter { get; set; }
        public DateTime? NextPEDateFilter { get; set; }
    }
}
