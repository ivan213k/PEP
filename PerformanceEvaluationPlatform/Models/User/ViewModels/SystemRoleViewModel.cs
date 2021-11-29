using PerformanceEvaluationPlatform.Application.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.ViewModels
{
    public class SystemRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static SystemRoleViewModel AsViewModel(this SystemRoleDto systemRole)
        {
            return new SystemRoleViewModel()
            {
                Id = systemRole.Id,
                Name = systemRole.Name
            };
        }
    }
}
