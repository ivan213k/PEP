using FormsData = PerformanceEvaluationPlatform.DAL.Models.FormData.Dao.FormData;
using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dao
{
    public class FieldData
    {
        public int Id { get; set; }
        public int FormDataId { get; set; }
        public int FieldId { get; set; }
        public int AssesmentId { get; set; }
        public string Comment { get; set; }
        public int Order { get; set; }

        public FormsData FormsData { get; set; }
        public Field Field { get; set; }

        public Assesment Assesment { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var formDataTypeBuilder = modelBuilder.Entity<FieldData>();
            formDataTypeBuilder.ToTable("FieldData");
            formDataTypeBuilder.HasKey(fd => fd.Id);
            formDataTypeBuilder.Property(fd => fd.FormDataId).IsRequired();
            formDataTypeBuilder.Property(fd => fd.FieldId).IsRequired();
            formDataTypeBuilder.Property(fd => fd.AssesmentId).IsRequired();
            formDataTypeBuilder.Property(fd => fd.Comment).IsRequired();
            formDataTypeBuilder.Property(fd => fd.Order).IsRequired();

            formDataTypeBuilder.HasOne(fd => fd.Field)
                .WithMany()
                .HasForeignKey(fd => fd.FieldId)
                .IsRequired();
            formDataTypeBuilder.HasOne(fd => fd.Assesment)
                .WithMany()
                .HasForeignKey(fd => fd.AssesmentId)
                .IsRequired();

        }


    }
}
