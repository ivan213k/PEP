using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Fields;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Fields
{
    public class AssesmentConfiguration : IEntityTypeConfiguration<Assesment>
    {
        public void Configure(EntityTypeBuilder<Assesment> modelBuilder)
        {
            modelBuilder.ToTable("Assesment");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
            modelBuilder.Property(t => t.AssesmentGroupId).IsRequired();

            modelBuilder.HasOne(fd => fd.AssesmentGroup)
                .WithMany()
                .HasForeignKey(fd => fd.AssesmentGroupId)
                .IsRequired();
        }
    }
}
