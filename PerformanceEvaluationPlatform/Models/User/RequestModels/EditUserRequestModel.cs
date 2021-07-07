using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class EditUserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Email]
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
    }
}
