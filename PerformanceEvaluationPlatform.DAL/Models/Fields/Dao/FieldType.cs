using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dao
{
    public class FieldType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var fieldTypeBuilder = modelBuilder.Entity<FieldType>();
            fieldTypeBuilder.ToTable("FieldType");
            fieldTypeBuilder.HasKey(t => t.Id);
            fieldTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
        }
    }
}
