using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Teams.Dao
{
    public class Team
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var teamTypeBuilder = modelBuilder.Entity<Team>();
            teamTypeBuilder.ToTable("Team");
            teamTypeBuilder.HasKey(t => t.Id);
            teamTypeBuilder.Property(t => t.Title).IsRequired().HasMaxLength(128);
            teamTypeBuilder.Property(t => t.ProjectId).IsRequired();

            //TODO: add Project and ICollection<User> navigation properties
        }
    }
}
