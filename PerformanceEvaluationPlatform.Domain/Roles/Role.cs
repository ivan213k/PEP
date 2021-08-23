using PerformanceEvaluationPlatform.Domain.Users;
using System.Collections.Generic;


namespace PerformanceEvaluationPlatform.Domain.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }

        public IEnumerable<UserRoleMap> UserRoleMaps { get; set; }
    }
}
