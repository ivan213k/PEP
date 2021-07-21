using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.DAL.Models.Examples.Dao
{
    public class ExampleState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Example> Examples { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var exampleTypeBuilder = modelBuilder.Entity<ExampleState>();
            exampleTypeBuilder.ToTable("ExampleState");
            exampleTypeBuilder.HasKey(t => t.Id);
            exampleTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            exampleTypeBuilder.HasMany<Example>().WithOne(t => t.ExampleState)
                .HasForeignKey(t => t.ExampleStateId).IsRequired();
        }
    }
}
