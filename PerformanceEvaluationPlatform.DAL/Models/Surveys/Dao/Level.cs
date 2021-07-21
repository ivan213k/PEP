using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public static void Configure(ModelBuilder modelBuilder)
        {
            var levelTypeBuilder = modelBuilder.Entity<Level>();
            levelTypeBuilder.ToTable(nameof(Level));
            levelTypeBuilder.HasKey(t => t.Id);
            levelTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            levelTypeBuilder.HasMany<Survey>().WithOne(t => t.RecomendedLevel)
               .HasForeignKey(t => t.RecommendedLevelId).IsRequired();
        }
    }
}
