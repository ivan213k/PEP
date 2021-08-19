using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Fields;
using PerformanceEvaluationPlatform.Domain.FormTemplates;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.FormTemplates
{
    public class FormTemplateFieldMapConfiguration: IEntityTypeConfiguration<FormTemplateFieldMap>
    {
        public void Configure (EntityTypeBuilder<FormTemplateFieldMap> builder)
        {
            builder.ToTable("FormTemplateFieldMap");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FormTemplateId).IsRequired();
            builder.Property(m => m.FieldId).IsRequired();
            builder.Property(m => m.Order).IsRequired();

            builder.HasOne<FormTemplate>()
                .WithMany(t => t.FormTemplateFieldMaps)
                .HasForeignKey(m => m.FormTemplateId)
                .IsRequired();

            builder.HasOne<Field>(m => m.Field)
                .WithMany()
                .HasForeignKey(m => m.FieldId)
                .IsRequired();
        }
    }
}
