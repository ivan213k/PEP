using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Users.Dao
{
    public class SystemRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public static void Configure(ModelBuilder modelBuilder)
        {
            var systemRoleTable = modelBuilder.Entity<SystemRole>();
            systemRoleTable.ToTable("SystemRole");

            systemRoleTable.HasKey(s=>s.Id);

            systemRoleTable.Property(s => s.Id).HasMaxLength(40);
            systemRoleTable.Property(s => s.Name).IsRequired().HasMaxLength(40);
        }
    }
}
