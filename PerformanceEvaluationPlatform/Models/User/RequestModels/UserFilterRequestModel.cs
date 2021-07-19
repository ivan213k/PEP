using PerformanceEvaluationPlatform.Models.Shared;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class UserFilterRequestModel : BaseFilterRequestModel
    {
        public string EmailOrName { get; set; }
        public ICollection<int> StateIds { get; set; }
        public ICollection<int> RoleIds { get; set; }
        public DateTime? PreviousPEDate { get; set; }
        public DateTime? NextPEDate { get; set; }

        public UserFilterRequestModel()
        {
            Skip = 1;
            Take = 2;
        }
    }
}
