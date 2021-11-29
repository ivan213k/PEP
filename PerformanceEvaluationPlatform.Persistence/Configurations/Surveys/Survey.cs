using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Surveys;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Surveys
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.ToTable(nameof(Survey));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.AppointmentDate).IsRequired();
            builder.Property(t => t.AssigneeId).IsRequired();
            builder.Property(t => t.SupervisorId).IsRequired();
            builder.Property(t => t.FormTemplateId).IsRequired();
            builder.Property(t => t.StateId).IsRequired();
            builder.Property(t => t.RecommendedLevelId).IsRequired();

            builder.HasOne(t => t.SurveyState)
                .WithMany()
                .HasForeignKey(t => t.StateId)
                .IsRequired();

            builder.HasOne(t => t.RecomendedLevel)
                .WithMany()
                .HasForeignKey(t => t.RecommendedLevelId)
                .IsRequired();

            builder.HasMany(t => t.DeepLinks)
                .WithOne()
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();

            builder.HasOne(t => t.Asignee)
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .IsRequired();

            builder.HasOne(t => t.Supervisor)
                .WithMany()
                .HasForeignKey(t => t.SupervisorId)
                .IsRequired();

            builder.HasOne(t => t.FormTemplate)
                .WithMany()
                .HasForeignKey(t => t.FormTemplateId)
                .IsRequired();

            builder.HasMany(t => t.FormData)
                .WithOne(f => f.Survey)
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();
        }
    }
}
