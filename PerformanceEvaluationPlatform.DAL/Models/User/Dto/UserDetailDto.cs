using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.User.Dto
{
    public class UserDetailDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<string> Role { get; set; }
        public string EnglishLevel { get; set; }
        public string TechnicalLevel { get; set; }
        public string Team { get; set; }
        public string Project { get; set; }
        public DateTime? NextPeDate { get; set; }
        public DateTime? PreviousPEDate { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public int YearsInCompany { get; set; }
        public int YearsOfExpirience { get; set; }
        public string State { get; set; }
        public ICollection<DateTime> PreviousPes{ get; set; }
    }
}
