using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Fields;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Fields
{
    public class FieldDataConfiguration : IEntityTypeConfiguration<FieldData>
    {
        public void Configure(EntityTypeBuilder<FieldData> modelBuilder)
        {
            modelBuilder.ToTable("FieldData");
            modelBuilder.HasKey(fd => fd.Id);
            modelBuilder.Property(fd => fd.FormDataId).IsRequired();
            modelBuilder.Property(fd => fd.FieldId).IsRequired();
            modelBuilder.Property(fd => fd.AssesmentId).IsRequired();
            modelBuilder.Property(fd => fd.Comment).IsRequired();
            modelBuilder.Property(fd => fd.Order).IsRequired();

            modelBuilder.HasOne(fd => fd.Field)
                .WithMany()
                .HasForeignKey(fd => fd.FieldId)
                .IsRequired();
            modelBuilder.HasOne(fd => fd.Assesment)
                .WithMany()
                .HasForeignKey(fd => fd.AssesmentId)
                .IsRequired();
            modelBuilder.HasOne(fd => fd.FormData)
                .WithMany(fd => fd.FieldData)
                .HasForeignKey(fd => fd.FormDataId)
                .IsRequired();
        }
    }
}
