using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Examples;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Examples
{
    public class ExampleConfiguration : IEntityTypeConfiguration<Example>
    {
        public void Configure(EntityTypeBuilder<Example> builder)
        {
            builder.ToTable("Example");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(256);
            builder.Property(t => t.ExampleStateId).IsRequired();
            builder.Property(t => t.ExampleTypeId).IsRequired();

            builder.HasOne<ExampleState>(t => t.ExampleState)
                .WithMany(t => t.Examples)
                .HasForeignKey(t => t.ExampleStateId)
                .IsRequired();

            builder.HasOne<ExampleType>(t => t.ExampleType)
                .WithMany()
                .HasForeignKey(t => t.ExampleTypeId)
                .IsRequired();

        }
    }
}
