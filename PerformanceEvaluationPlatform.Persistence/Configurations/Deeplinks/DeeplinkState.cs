using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Deeplinks;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Deeplinks
{
    public class DeeplinkStateConfiguration : IEntityTypeConfiguration<DeeplinkState>
    {
        public void Configure(EntityTypeBuilder<DeeplinkState> deeplinkBuilder)
        {
            deeplinkBuilder.ToTable("DeeplinkState");
            deeplinkBuilder.HasKey(t => t.Id);

            deeplinkBuilder.Property(t => t.Name)
                .IsRequired().
                HasMaxLength(128);
            deeplinkBuilder.HasMany(t => t.Deeplinks)
                .WithOne(t => t.DeeplinkState)
                .HasForeignKey(t => t.StateId).IsRequired();

        }
    }
}
