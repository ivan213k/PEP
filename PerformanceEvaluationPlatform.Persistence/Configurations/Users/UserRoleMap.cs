using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Users;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Users
{
    class UserRoleMapConfiguration : IEntityTypeConfiguration<UserRoleMap>
    {
        public void Configure(EntityTypeBuilder<UserRoleMap> builder)
        {
            builder.ToTable("UserRoleMap");

            builder.HasKey(s => s.Id);

        }
    }
}
