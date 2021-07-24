using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Roles.Dto
{
    public class RoleListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }
}
