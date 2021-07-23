using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao
{
    public class SurveyState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var surveyStateTypeBuilder = modelBuilder.Entity<SurveyState>();
            surveyStateTypeBuilder.ToTable(nameof(SurveyState));
            surveyStateTypeBuilder.HasKey(t => t.Id);
            surveyStateTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            surveyStateTypeBuilder.HasMany<Survey>().WithOne(t => t.SurveyState)
                .HasForeignKey(t => t.StateId).IsRequired();
        }
    }
}
