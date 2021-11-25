using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Projects;
using PerformanceEvaluationPlatform.Domain.Users;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Projects
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(128);
            builder.Property(t => t.StartDate).IsRequired();
            builder.Property(t => t.CoordinatorId).IsRequired();

            builder.HasOne<User>(t => t.Coordinator)
                .WithMany()
                .HasForeignKey(t => t.CoordinatorId)
                .IsRequired();
        }
    }
}
