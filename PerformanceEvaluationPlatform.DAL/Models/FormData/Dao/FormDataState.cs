using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.FormData.Dao
{
    public class FormDataState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var formDataTypeBuilder = modelBuilder.Entity<FormDataState>();
            formDataTypeBuilder.ToTable("FormDataState");
            formDataTypeBuilder.HasKey(fds => fds.Id);
            formDataTypeBuilder.Property(fds => fds.Name).IsRequired().HasMaxLength(128);

            formDataTypeBuilder.HasMany<FormData>()
                .WithOne(td => td.FormDataState)
                .HasForeignKey(td => td.FormDataStateId)
                .IsRequired();
        }
    }
}
