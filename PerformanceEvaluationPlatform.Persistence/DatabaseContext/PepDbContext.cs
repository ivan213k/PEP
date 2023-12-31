﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Domain.Shared;
using PerformanceEvaluationPlatform.Persistence.Configurations.Deeplinks;
using PerformanceEvaluationPlatform.Persistence.Configurations.Documents;
using PerformanceEvaluationPlatform.Persistence.Configurations.Examples;
using PerformanceEvaluationPlatform.Persistence.Configurations.Fields;
using PerformanceEvaluationPlatform.Persistence.Configurations.FormsData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Persistence.Configurations.FormTemplates;
using PerformanceEvaluationPlatform.Persistence.Configurations.Surveys;
using PerformanceEvaluationPlatform.Persistence.Configurations.Roles;
using PerformanceEvaluationPlatform.Persistence.Configurations.Users;
using PerformanceEvaluationPlatform.Persistence.Configurations.Projects;

namespace PerformanceEvaluationPlatform.Persistence.DatabaseContext
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
	        modelBuilder.ApplyConfiguration(new ExampleTypeConfiguration());
	        modelBuilder.ApplyConfiguration(new ExampleStateConfiguration());
	        modelBuilder.ApplyConfiguration(new ExampleConfiguration());

            modelBuilder.ApplyConfiguration(new FormDataStateConfiguration());
            modelBuilder.ApplyConfiguration(new FormDataConfiguration());
            modelBuilder.ApplyConfiguration(new DeeplinkConfiguration());
            modelBuilder.ApplyConfiguration(new DeeplinkStateConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleMapConfiguration());
            modelBuilder.ApplyConfiguration(new UserStateConfiguration());

            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());

            //FormData.Configure(modelBuilder);
            //FormDataState.Configure(modelBuilder);

            modelBuilder.ApplyConfiguration(new SurveyConfiguration());
            modelBuilder.ApplyConfiguration(new SurveyStateConfiguration());
            modelBuilder.ApplyConfiguration(new LevelConfiguration());

            modelBuilder.ApplyConfiguration(new AssesmentConfiguration());
            modelBuilder.ApplyConfiguration(new FieldAssesmentGroupConfiguration());
            modelBuilder.ApplyConfiguration(new FieldConfiguration());
            modelBuilder.ApplyConfiguration(new FieldDataConfiguration());
            modelBuilder.ApplyConfiguration(new FieldTypeConfiguration());

            modelBuilder.ApplyConfiguration(new FormTemplateStatusConfiguration());
            modelBuilder.ApplyConfiguration(new FormTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new FormTemplateFieldMapConfiguration());

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());

            modelBuilder.ApplyConfiguration(new SystemRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserStateConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleMapConfiguration());

            //FormData.Configure(modelBuilder);
            //FormDataState.Configure(modelBuilder);

            //Survey.Configure(modelBuilder);
            //SurveyState.Configure(modelBuilder);
            //Level.Configure(modelBuilder);

            //Assesment.Configure(modelBuilder);
            //FieldData.Configure(modelBuilder);
            //Field.Configure(modelBuilder);
            //FieldType.Configure(modelBuilder);
            //FieldAssesmentGroup.Configure(modelBuilder);

            //FormTemplate.Configure(modelBuilder);
            //FormTemplateStatus.Configure(modelBuilder);
            //FormTemplateFieldMap.Configure(modelBuilder);

            //Role.Configure(modelBuilder);

            var allEntityies = modelBuilder.Model.GetEntityTypes();
            foreach (var entity in allEntityies)
            {
                if (IsAssignebleFrom(entity.ClrType, typeof(IUpdatebleCreateable)))
                {
                    entity.AddProperty("CreatedAt", typeof(DateTime));
                    entity.AddProperty("LastUpdatesAt", typeof(DateTime?));
                }
            }
            //FieldGroup.Configure(modelBuilder);
            //Deeplink.Configure(modelBuilder);
            //DeeplinkState.Configure(modelBuilder);

            //Team.Configure(modelBuilder);

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
