﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Users
{
    public class UserRoleMapConfiguration:IEntityTypeConfiguration<UserRoleMap>
    {
        public void Configure(EntityTypeBuilder<UserRoleMap> builder)
        {
            builder.ToTable("UserRoleMap");

            builder.HasKey(s => s.Id);

        }
    }
}
