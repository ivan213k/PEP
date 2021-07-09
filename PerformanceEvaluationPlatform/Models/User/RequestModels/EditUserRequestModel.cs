using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class EditUserRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string LevelName { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string EnglishLevelName { get; set; }
        [Required]
        public DateTime NextPEDate { get; set; }
        [Required]
        public DateTime FirstDayInCompany { get; set; }
    }
}
