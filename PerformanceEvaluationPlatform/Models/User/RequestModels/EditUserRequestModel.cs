using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class EditUserRequestModel:IUserRequest
    {
        [Required]
        [MaxLength(70)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(120)]
        public string LastName { get; set; }
        [EmailAddress]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        public List<int> RoleIds { get; set; }
        [Required]
        public int TeamId{ get; set; }
        [Required]
        public int TechnicalLevelId{ get; set; }
        [Required]
        public int EnglishLevelId { get; set; }
        [Required]
        public string SystemRoleId { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        [Required]
        public DateTime FirstDayInIndustry { get; set; }
    }
}
