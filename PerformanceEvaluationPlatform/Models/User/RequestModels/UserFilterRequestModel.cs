using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class UserFilterRequestModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
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
