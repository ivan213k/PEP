using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.RequestModels
{
    public class CreateUserRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int EnglishLevelId { get; set; }
        [Required]
        public int YearsOfExpirience { get; set; }
        [Required]
        public DateTime NextPEDate { get; set; }
        [Required]
        public DateTime FirstDayInCompany { get; set; }
    }
}
