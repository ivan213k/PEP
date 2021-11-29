using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Surveys;
using PerformanceEvaluationPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Users
{
   public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(s => s.Id);
            builder.Property(fn => fn.FirstName)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(ln => ln.LastName)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(fdi => fdi.FirstDayInIndustry)
                .IsRequired();

            builder.Property(fdi => fdi.FirstDayInCompany)
               .IsRequired();

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(40);

            builder.HasOne(us => us.UserState)
                .WithMany()
                .HasForeignKey(s => s.StateId)
                .IsRequired();

            builder.HasOne(l=>l.TechnicalLevel)
               .WithMany()
               .HasForeignKey(s => s.TechnicalLevelId)
               .IsRequired();

            //builder.HasOne(t => t.Team)
            //    .WithMany()
            //    .HasForeignKey(t => t.TeamId)
            //    .IsRequired();

            builder.HasOne(t => t.EnglishLevel)
                .WithMany()
                .HasForeignKey(T => T.EnglishLevelId)
                .IsRequired();

            builder.HasMany(s => s.Surveys)
                .WithOne(s => s.Asignee)
                .IsRequired();

            builder.HasOne(s => s.SystemRole)
                .WithMany()
                .HasForeignKey(s => s.SystemRoleId)
                .IsRequired();
        }
    }
}
