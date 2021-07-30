using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Users.Dto
{
   public  class UserListItemDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TeamName { get; set; }
        public string StateName { get; set; }
        public string RoleName { get; set; }
        public string LevelName { get; set; }
        public DateTime PreviousPE { get; set; }
        public DateTime NextPE { get; set; }

    }
}
