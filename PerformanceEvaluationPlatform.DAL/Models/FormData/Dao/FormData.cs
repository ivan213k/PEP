using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.FormData.Dao
{
    public class FormData
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public int FormDataStateId { get; set; }

        public FormDataState FormDataState { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var formDataTypeBuilder = modelBuilder.Entity<FormData>();
            formDataTypeBuilder.ToTable("FormData");
            formDataTypeBuilder.HasKey(fd => fd.Id);
            formDataTypeBuilder.Property(fd => fd.FormDataStateId).IsRequired();
            formDataTypeBuilder.Property(fd => fd.UserId).IsRequired();
            formDataTypeBuilder.Property(fd => fd.SurveyId).IsRequired();

            formDataTypeBuilder.HasOne(fd => fd.FormDataState)
                .WithMany()
                .HasForeignKey(fd => fd.FormDataStateId)
                .IsRequired();
        }
    }
}
