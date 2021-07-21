using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Examples.Dao
{
    public class ExampleType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var exampleTypeBuilder = modelBuilder.Entity<ExampleType>();
            exampleTypeBuilder.ToTable("ExampleType");
            exampleTypeBuilder.HasKey(t => t.Id);
            exampleTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
        }
    }
}
