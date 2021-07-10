using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.ViewModels
{
    public class UserDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TeamName { get; set; }

        public string LevelName { get; set; }
        public string StateName { get; set; }

        public string RoleName { get; set; }

        public string ProjectName { get; set; }

        public string EnglishLevelName { get; set; }

        public int YearsInCompany { get; set; }
       
        public int YearsOfExpirience { get; set; }

        public DateTime PreviousPEDate { get; set; }
        public DateTime NextPEDate { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public ICollection<DateTime> PreviousPEs { get; set; }
    }
}
