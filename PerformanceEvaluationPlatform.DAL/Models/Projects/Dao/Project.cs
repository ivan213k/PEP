using Microsoft.EntityFrameworkCore;
using System;

namespace PerformanceEvaluationPlatform.DAL.Models.Projects.Dao
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int CoordinatorId { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var projectTypeBuilder = modelBuilder.Entity<Project>();
            projectTypeBuilder.ToTable("Project");
            projectTypeBuilder.HasKey(t => t.Id);
            projectTypeBuilder.Property(t => t.Title).IsRequired().HasMaxLength(128);
            projectTypeBuilder.Property(t => t.StartDate).IsRequired();
            projectTypeBuilder.Property(t => t.CoordinatorId).IsRequired();
        }
    }
}
