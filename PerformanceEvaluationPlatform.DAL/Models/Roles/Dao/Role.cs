using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Roles.Dao
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }

        public IEnumerable<UserRoleMap> UserRoleMaps { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var roleTypeBuilder = modelBuilder.Entity<Role>();
            roleTypeBuilder.ToTable("Role");
            roleTypeBuilder.HasKey(t => t.Id);
            roleTypeBuilder.Property(t => t.Title).IsRequired().HasMaxLength(256);
            roleTypeBuilder.Property(t => t.IsPrimary).IsRequired();
        }
    }
}
