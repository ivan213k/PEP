using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class UserSortingRequestModel
    {
        public byte UserName { get; set; }
        public byte  UserPreviousPE { get; set; }
        public byte  UserNextPE { get; set; }

    }
}
