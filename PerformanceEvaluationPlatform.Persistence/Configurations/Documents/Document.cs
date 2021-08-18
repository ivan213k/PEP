using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Documents;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Documents
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Document");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UserId)
                .IsRequired();
            builder.Property(t => t.TypeId)
                .IsRequired();
            builder.Property(t => t.ValidToDate)
                .IsRequired();
            builder.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(t => t.CreatedById)
                .IsRequired();
            builder.Property(t => t.MetaData)
                .IsRequired();


            builder.HasOne(t => t.DocumentType)
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired();
            //builder.HasOne(t => t.User)
            //    .WithMany()
            //    .HasForeignKey(t => t.UserId)
            //    .IsRequired();
            //builder.HasOne(t => t.CreatedBy)
            //    .WithMany()
            //    .HasForeignKey(t => t.CreatedById)
            //    .IsRequired();
            //builder
            //    .HasOne(t => t.UpdatedBy)
            //    .WithMany()
            //.HasForeignKey(t => t.LastUpdatesById);


        }
    }
}
