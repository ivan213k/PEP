using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.FormTemplates;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.FormTemplates
{
    public class FormTemplateConfiguration: IEntityTypeConfiguration<FormTemplate>
    {
        public void Configure(EntityTypeBuilder<FormTemplate> builder)
        {
            builder.ToTable("FormTemplate");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128).IsRequired();
            builder.Property(t => t.Version).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.StatusId).IsRequired();

            builder.HasOne<FormTemplateStatus>(t => t.FormTemplateStatus)
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .IsRequired();

            builder.HasMany<FormTemplateFieldMap>()
                .WithOne()
                .HasForeignKey(m => m.FormTemplateId)
                .IsRequired();
        }
    }
}
