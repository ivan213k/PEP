using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dao
{
    public class FieldGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public static void Configure (ModelBuilder modelBuilder)
        {
            var fieldGroupBuilder = modelBuilder.Entity<FieldGroup>();
            fieldGroupBuilder.ToTable("FieldGroup");
            fieldGroupBuilder.HasKey(t => t.Id);
            fieldGroupBuilder.Property(t => t.Title).IsRequired().HasMaxLength(256);
        }
    }
}
