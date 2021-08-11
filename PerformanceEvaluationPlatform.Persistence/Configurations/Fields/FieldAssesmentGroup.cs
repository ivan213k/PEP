using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.Domain.Fields;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Fields
{
    public class FieldAssesmentGroupConfiguration : IEntityTypeConfiguration<FieldAssesmentGroup>
    {
        public void Configure(EntityTypeBuilder<FieldAssesmentGroup> modelBuilder)
        {
            modelBuilder.ToTable("AssesmentGroup");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
        }
    }
}
