using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.FormsData;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.FormsData
{
    public class FormDataStateConfiguration : IEntityTypeConfiguration<FormDataState>
    {
        public void Configure(EntityTypeBuilder<FormDataState> builder)
        {
            builder.ToTable("FormDataState");
            builder.HasKey(fds => fds.Id);
            builder.Property(fds => fds.Name).IsRequired().HasMaxLength(128);
        }
    }
}