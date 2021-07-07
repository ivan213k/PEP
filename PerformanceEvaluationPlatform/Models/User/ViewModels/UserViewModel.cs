using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.ViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //Повиннен получати доступ до імені команди з змінної типу Team, поки що так 
        public string TeamName { get; set; }

        public int StateId { get; set; }
        public string State { get; set; }

        public int LevelId { get; set; }
        public string Level { get; set; }

        public int RoleId { get; set; }
        public string Role { get; set; }

        public DateTime PreviousPEDate { get; set; }
        public DateTime NextPEDate { get 
            {
                return PreviousPEDate.AddMonths(5);
            } } 

    }
}
