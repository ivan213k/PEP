using PerformanceEvaluationPlatform.Domain.Roles;

namespace PerformanceEvaluationPlatform.Domain.Users
{
    public class UserRoleMap
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
