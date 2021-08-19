using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Surveys;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Surveys
{
    public class SurveyStateConfiguration : IEntityTypeConfiguration<SurveyState>
    {
        public void Configure(EntityTypeBuilder<SurveyState> builder)
        {
            builder.ToTable(nameof(SurveyState));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            builder.HasMany<Survey>().WithOne(t => t.SurveyState)
                .HasForeignKey(t => t.StateId).IsRequired();
        }
    }
}
