using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public DateTime FirstDayInIndustry { get; set; }
        public int TeamId { get; set; }
        public int StateId { get; set; }
        public UserState UserState { get; set; }
        public int TechnicalLevelId { get; set; }
        public int EnglishLevelId { get; set; }
        public IEnumerable<UserRoleMap> Roles { get; set; }
    }
}
