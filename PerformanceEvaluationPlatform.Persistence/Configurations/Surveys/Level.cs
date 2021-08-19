using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Surveys;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Surveys
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable(nameof(Level));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            builder.HasMany<Survey>().WithOne(t => t.RecomendedLevel)
               .HasForeignKey(t => t.RecommendedLevelId).IsRequired();
        }
    }
}
