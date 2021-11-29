using PerformanceEvaluationPlatform.Application.Model.Users;
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
        public string TeamName { get; set; }

        public string StateName { get; set; }

        public string LevelName { get; set; }

        public string RoleName { get; set; }

        public DateTime PreviousPEDate { get; set; }
        public DateTime NextPEDate { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static UserViewModel AsViewModel(this UserListItemDto userDto)
        {
            return new UserViewModel()
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                Id = userDto.Id,
                LastName = userDto.LastName,
                LevelName = userDto.LastName,
                NextPEDate = userDto.NextPE,
                PreviousPEDate = userDto.PreviousPE,
                RoleName = userDto.RoleName,
                StateName = userDto.StateName,
                TeamName = userDto.TeamName
            };
        }
    }
}
