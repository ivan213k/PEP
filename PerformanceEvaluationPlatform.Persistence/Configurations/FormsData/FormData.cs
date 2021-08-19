using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Fields;
using PerformanceEvaluationPlatform.Domain.FormsData;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.FormsData
{
    public class FormDataConfiguration : IEntityTypeConfiguration<FormData>
    {
        public void Configure(EntityTypeBuilder<FormData> builder)
        {
            builder.ToTable("FormData");
            builder.HasKey(fd => fd.Id);
            builder.Property(fd => fd.UserId).IsRequired();
            builder.Property(fd => fd.SurveyId).IsRequired();
            builder.Property(fd => fd.FormDataStateId).IsRequired();

            builder.HasOne(fd => fd.FormDataState)
                .WithMany()
                .HasForeignKey(fd => fd.FormDataStateId)
                .IsRequired();
            builder.HasMany(t => t.FieldData)
                .WithOne()
                .HasForeignKey(t => t.FormDataId)
                .IsRequired();
            builder.HasOne(fd => fd.Survey)
                .WithMany(s => s.FormData)
                .HasForeignKey(fd => fd.SurveyId)
                .IsRequired();
            //builder.HasOne(fd => fd.User)
            //    .WithMany()
            //    .HasForeignKey(fd => fd.UserId)
            //    .IsRequired();

        }
    }
}