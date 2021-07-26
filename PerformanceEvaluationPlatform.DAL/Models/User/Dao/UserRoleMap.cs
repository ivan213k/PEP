using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.User.Dao
{
    public class UserRoleMap
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var userRoleMapTableBuilder = modelBuilder.Entity<UserRoleMap>();
            userRoleMapTableBuilder.ToTable("UserRoleMap");

            userRoleMapTableBuilder.HasKey(s => s.Id);

            userRoleMapTableBuilder.Property(ui => ui.UserId)
                .IsRequired();

            userRoleMapTableBuilder.Property(ri => ri.RoleId)
                .IsRequired();

            userRoleMapTableBuilder.HasOne(ui => ui.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            userRoleMapTableBuilder.HasOne(r => r.Role)
                .WithMany()
                .HasForeignKey(s => s.RoleId)
                .IsRequired();


        }

    }
}
