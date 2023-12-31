﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Shared;
using PerformanceEvaluationPlatform.DAL.Models.Teams.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.DatabaseContext
{
    public class PepDbContext : DbContext
    {
        private readonly DatabaseOptions _options;

        public PepDbContext(IOptions<DatabaseOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(_options.SqlConnectionString))
            {
                throw new ArgumentException("Connection string is null or empty");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.SqlConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            Assesment.Configure(modelBuilder);
            FieldData.Configure(modelBuilder);
            Field.Configure(modelBuilder);
            FieldType.Configure(modelBuilder);
            FieldAssesmentGroup.Configure(modelBuilder);

            FormTemplate.Configure(modelBuilder);
            FormTemplateStatus.Configure(modelBuilder);
            FormTemplateFieldMap.Configure(modelBuilder);

            SystemRole.Configure(modelBuilder);
            User.Configure(modelBuilder);
            UserState.Configure(modelBuilder);
            UserRoleMap.Configure(modelBuilder);

            var allEntityies = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in allEntityies) 
            {
                if (IsAssignebleFrom(entity.ClrType, typeof(IUpdatebleCreateable))) 
                {
                    entity.AddProperty("CreatedAt", typeof(DateTime));
                    entity.AddProperty("LastUpdatesAt", typeof(DateTime?));
                }
            }
            FieldGroup.Configure(modelBuilder);

            Team.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                                        .Where(e => (e.State == EntityState.Added ||
                                                     e.State == EntityState.Modified) &&
                                                     IsAssignebleFrom(e.Entity.GetType(), typeof(IUpdatebleCreateable)));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("LastUpdatesAt").CurrentValue = DateTime.Now;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private bool IsAssignebleFrom(Type entity, Type assigneFromType) 
        {
            return assigneFromType.IsAssignableFrom(entity);
        }
    }
}
