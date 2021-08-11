using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.Domain.FormTemplates;
using PerformanceEvaluationPlatform.Domain.Fields;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Fields
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> modelBuilder)
        {
            modelBuilder.ToTable("Field");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).IsRequired().HasMaxLength(256);
            modelBuilder.Property(t => t.FieldTypeId).IsRequired();
            modelBuilder.Property(t => t.AssesmentGroupId).IsRequired();
            modelBuilder.Property(t => t.Description).IsRequired().HasMaxLength(256);

            modelBuilder.HasOne<FieldType>(t => t.FieldType)
                .WithMany()
                .HasForeignKey(t => t.FieldTypeId)
                .IsRequired();

            modelBuilder.HasOne<FieldAssesmentGroup>(t => t.AssesmentGroup)
                .WithMany()
                .HasForeignKey(t => t.AssesmentGroupId)
                .IsRequired();

            modelBuilder.HasMany<FormTemplateFieldMap>()
                .WithOne(m=>m.Field)
                .HasForeignKey(m => m.FieldId)
                .IsRequired();

        }
    }
}
