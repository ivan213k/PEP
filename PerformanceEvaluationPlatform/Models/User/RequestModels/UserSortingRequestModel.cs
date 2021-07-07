using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class UserSortingRequestModel
    {
        public byte UserNameSorting { get; set; }
        public byte  UserPreviousPESorting { get; set; }
        public byte  UserNextPESorting { get; set; }

    }
}
