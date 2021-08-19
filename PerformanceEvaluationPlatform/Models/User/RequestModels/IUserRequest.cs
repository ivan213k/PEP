using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    interface IUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<int> RoleIds { get; set; }
        public int TeamId { get; set; }
        public int TechnicalLevelId { get; set; }
        public int EnglishLevelId { get; set; }
        public string SystemRoleId { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public DateTime FirstDayInIndustry { get; set; }
    }
}
