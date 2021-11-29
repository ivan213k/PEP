using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Users
{
    public class SystemRoleConfiguration: IEntityTypeConfiguration<SystemRole>
    {
        public void Configure(EntityTypeBuilder<SystemRole> builder)
        {
            builder.ToTable("SystemRole");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasMaxLength(40);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(40);
        }
    }
}
