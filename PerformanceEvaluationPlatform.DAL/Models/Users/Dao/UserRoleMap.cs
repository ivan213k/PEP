using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.Domain.Roles;

namespace PerformanceEvaluationPlatform.DAL.Models.Users.Dao
{
    public class UserRoleMap
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var userRoleMapTableBuilder = modelBuilder.Entity<UserRoleMap>();
            userRoleMapTableBuilder.ToTable("UserRoleMap");

            userRoleMapTableBuilder.HasKey(s => s.Id);

        }

    }
}
