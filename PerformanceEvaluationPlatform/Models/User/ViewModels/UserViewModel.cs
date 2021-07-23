using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //Повиннен получати доступ до імені команди з змінної типу Team, поки що так 
        public string TeamName { get; set; }

        public string StateName { get; set; }

        public string LevelName { get; set; }

        public string RoleName { get; set; }

        public DateTime PreviousPEDate { get; set; }
        public DateTime NextPEDate { get; set; }
           

    }
}
