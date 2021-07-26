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
        public List<int> RoleIds { get; set; }
        [Required]
        public int TeamId{ get; set; }
        [Required]
        public int TechnicalLevelId{ get; set; }
        public int EnglishLevelId { get; set; }
        [Required]
        public DateTime FirstDayInCompany { get; set; }
        [Required]
        public DateTime FirstDayInIndustry { get; set; }
    }
}
