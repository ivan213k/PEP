using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.Domain.Fields;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Fields
{
    public class FieldTypeConfiguration : IEntityTypeConfiguration<FieldType>
    {
        public void Configure(EntityTypeBuilder<FieldType> modelBuilder)
        {
            modelBuilder.ToTable("FieldType");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
        }
    }
}
