using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.FormTemplates;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.FormTemplates
{
    public class FormTemplateStatusConfiguration: IEntityTypeConfiguration<FormTemplateStatus>
    {
        public void Configure(EntityTypeBuilder<FormTemplateStatus> builder)
        {
            builder.ToTable("FormTemplateStatus");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(128);

            builder.HasMany<FormTemplate>()
                .WithOne(t => t.FormTemplateStatus)
                .HasForeignKey(t => t.StatusId)
                .IsRequired();
        }
    }
}
