using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Users;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Users
{
    class UserStatepConfiguration : IEntityTypeConfiguration<UserState>
    {
        public void Configure(EntityTypeBuilder<UserState> builder)
        {
            builder.ToTable("UserState");

            builder.HasKey(s => s.Id);

            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(60);

        }
    }
}
