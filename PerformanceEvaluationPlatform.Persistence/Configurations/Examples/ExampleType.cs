using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Examples;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Examples
{
    public class ExampleTypeConfiguration : IEntityTypeConfiguration<ExampleType>
    {
        public void Configure(EntityTypeBuilder<ExampleType> builder)
        {
            builder.ToTable("ExampleType");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);
        }
    }
}
