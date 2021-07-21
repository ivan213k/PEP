using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Examples.Dao
{
    public class Example
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ExampleTypeId { get; set; }
        public int ExampleStateId { get; set; }

        public ExampleType ExampleType { get; set; }
        public ExampleState ExampleState { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var exampleTypeBuilder = modelBuilder.Entity<Example>();
            exampleTypeBuilder.ToTable("Example");
            exampleTypeBuilder.HasKey(t => t.Id);
            exampleTypeBuilder.Property(t => t.Title).IsRequired().HasMaxLength(256);
            exampleTypeBuilder.Property(t => t.ExampleStateId).IsRequired();
            exampleTypeBuilder.Property(t => t.ExampleTypeId).IsRequired();

            exampleTypeBuilder.HasOne<ExampleState>(t => t.ExampleState)
                .WithMany(t => t.Examples)
                .HasForeignKey(t => t.ExampleStateId)
                .IsRequired();

            exampleTypeBuilder.HasOne<ExampleType>(t => t.ExampleType)
                .WithMany()
                .HasForeignKey(t => t.ExampleTypeId)
                .IsRequired();

        }
    }
}
