using PerformanceEvaluationPlatform.Application.Model.Users;
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

        public string TechnicalLevelName { get; set; }
        public string StateName { get; set; }

        public ICollection<string> RoleNames { get; set; }

        public string ProjectName { get; set; }

        public string EnglishLevelName { get; set; }

        public int YearsInCompany { get; set; }
       
        public int YearsOfExpirience { get; set; }

        public DateTime? PreviousPEDate { get; set; }
        public DateTime? NextPEDate { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public ICollection<DateTime> PreviousPEs { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static UserDetailViewModel AsViewModel (this UserDetailDto userDto)
        {
            return new UserDetailViewModel()
            {
                Email = userDto.Email,
                EnglishLevelName = userDto.EnglishLevel,
                FirstDayInCompany = userDto.FirstDayInCompany,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                NextPEDate = userDto.NextPeDate,
                PreviousPEDate = userDto.PreviousPEDate,
                PreviousPEs = userDto.PreviousPes,
                ProjectName = userDto.Project,
                RoleNames = userDto.Role,
                StateName = userDto.State,
                TeamName = userDto.Team,
                TechnicalLevelName = userDto.TechnicalLevel,
                YearsInCompany = userDto.YearsInCompany,
                YearsOfExpirience = userDto.YearsOfExpirience
            };
        }
    }
}
