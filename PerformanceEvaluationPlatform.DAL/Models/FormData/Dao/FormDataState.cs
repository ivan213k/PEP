using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.DAL.Models.FormData.Dao
{
    public class FormDataState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<FormData> FormData { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var FormDataTypeBuilder = modelBuilder.Entity<FormDataState>();
            FormDataTypeBuilder.ToTable("FormDataState");
            FormDataTypeBuilder.HasKey(fds => fds.Id);
            FormDataTypeBuilder.Property(fds => fds.Name).IsRequired().HasMaxLength(128);

            FormDataTypeBuilder.HasMany<FormData>().WithOne(td => td.FormDataState)
                .HasForeignKey(td => td.FormDataStateId).IsRequired();
        }
    }
}
