using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dao
{
    public class FieldAssesmentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var fieldAssesmentGroupBuilder = modelBuilder.Entity<FieldAssesmentGroup>();
            fieldAssesmentGroupBuilder.ToTable("AssesmentGroup");
            fieldAssesmentGroupBuilder.HasKey(t => t.Id);
            fieldAssesmentGroupBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
        }
    }
}
