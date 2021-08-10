using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Examples;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Examples
{
    public class ExampleStateConfiguration : IEntityTypeConfiguration<ExampleState>
    {
        public void Configure(EntityTypeBuilder<ExampleState> builder)
        {
            builder.ToTable("ExampleState");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            builder.HasMany<Example>().WithOne(t => t.ExampleState)
                .HasForeignKey(t => t.ExampleStateId).IsRequired();
        }
    }
}
