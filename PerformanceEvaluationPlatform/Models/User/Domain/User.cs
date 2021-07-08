using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //Повиннен получати доступ до імені команди з змінної типу Team, поки що так 
        public string TeamName { get; set; }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public int LevelId { get; set; }
        public string LevelName { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public int EnglishLevelId { get; set; }
        public string EnglishLevelName { get; set; }

        public int YearsInCompany { get { return
                PreviousPEDate.Year - FirstDayInCompany.Year;
            } }

        public int YearsOfExpirience { get; set; }


        public DateTime PreviousPEDate { get; set; }
        public DateTime NextPEDate { get; set; }
        public DateTime FirstDayInCompany { get; set; }

        public ICollection<DateTime> PreviousPEs { get; set; }
    }
}
