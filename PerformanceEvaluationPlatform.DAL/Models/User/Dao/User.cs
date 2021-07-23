using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.User.Dao
{
    class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime FirstDayInIndustry { get; set; }
        public DateTime FirstDayInCompany { get; set; }
        public int TeamId { get; set; }
        public int StateId { get; set; }
        public UserState UserState { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }


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

            userTableBuilder.Property(ti => ti.TeamId)
                .IsRequired();

            userTableBuilder.Property(ti => ti.StateId)
                .IsRequired();

            userTableBuilder.Property(ti => ti.LevelId)
                .IsRequired();

            userTableBuilder.HasOne(us => us.UserState)
                .WithMany()
                .HasForeignKey(s => s.StateId)
                .IsRequired();

            userTableBuilder.HasOne(l=>l.Level)
               .WithMany()
               .HasForeignKey(s => s.LevelId)
               .IsRequired();
        }
    }
}
