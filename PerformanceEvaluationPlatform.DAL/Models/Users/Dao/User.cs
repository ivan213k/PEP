using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Teams.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Users.Dao
{
   public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public Team Team { get; set; }
        public DateTime FirstDayInIndustry { get; set; }
        public int TeamId { get; set; }
        public int StateId { get; set; }
        public UserState UserState { get; set; }
        public int TechnicalLevelId { get; set; }
        public Level TechnicalLevel { get; set; }
        public int EnglishLevelId { get; set; }
        public Level EnglishLevel { get; set; }
        public int SystemRole { get; set; }
        public IEnumerable<UserRoleMap> Roles { get; set; }
        public IEnumerable<Survey> Surveys { get; set; }


        public static void Configure(ModelBuilder modelBuilder)
        {
            var userTableBuilder = modelBuilder.Entity<User>();
            userTableBuilder.ToTable("User");

            userTableBuilder.HasKey(s => s.Id);
            userTableBuilder.Property(fn => fn.FirstName)
                .IsRequired()
                .HasMaxLength(70);

            userTableBuilder.Property(ln => ln.LastName)
                .IsRequired()
                .HasMaxLength(120);

            userTableBuilder.Property(fdi => fdi.FirstDayInIndustry)
                .IsRequired();

            userTableBuilder.Property(fdi => fdi.FirstDayInCompany)
               .IsRequired();

            userTableBuilder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(40);

            userTableBuilder.HasOne(us => us.UserState)
                .WithMany()
                .HasForeignKey(s => s.StateId)
                .IsRequired();

            userTableBuilder.HasOne(l=>l.TechnicalLevel)
               .WithMany()
               .HasForeignKey(s => s.TechnicalLevelId)
               .IsRequired();

            userTableBuilder.HasOne(t => t.Team)
                .WithMany()
                .HasForeignKey(t => t.TeamId)
                .IsRequired();

            userTableBuilder.HasOne(t => t.EnglishLevel)
                .WithMany()
                .HasForeignKey(T => T.EnglishLevelId)
                .IsRequired();

            userTableBuilder.HasMany(s => s.Surveys)
                .WithOne(s => s.Asignee)
                .IsRequired();

        }
    }
}
