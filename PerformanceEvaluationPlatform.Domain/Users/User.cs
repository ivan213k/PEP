using PerformanceEvaluationPlatform.Domain.Surveys;
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
       // public Team Team { get; set; }
        public DateTime FirstDayInIndustry { get; set; }
        public int TeamId { get; set; }
        public int StateId { get; set; }
        public UserState UserState { get; set; }
        public int TechnicalLevelId { get; set; }
        public Level TechnicalLevel { get; set; }
        public int EnglishLevelId { get; set; }
        public Level EnglishLevel { get; set; }
        public string SystemRoleId { get; set; }
        public SystemRole SystemRole { get; set; }
        public IEnumerable<UserRoleMap> Roles { get; set; }
        public IEnumerable<Survey> Surveys { get; set; }
    }
}
