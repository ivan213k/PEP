using PerformanceEvaluationPlatform.Application.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.ViewModels
{
    public class UserStateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static UserStateViewModel AsViewModel(this UserStateListItemDto userStateDto)
        {
            return new UserStateViewModel()
            {
                Id = userStateDto.Id,
                Name = userStateDto.Name
            };
        }
    }
}
