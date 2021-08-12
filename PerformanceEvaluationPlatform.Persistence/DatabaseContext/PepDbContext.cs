using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Domain.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Persistence.Configurations.Examples;
using PerformanceEvaluationPlatform.Persistence.Configurations.Fields;
using PerformanceEvaluationPlatform.Persistence.Configurations.FormsData;

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

            //Survey.Configure(modelBuilder);
            //SurveyState.Configure(modelBuilder);
            //Level.Configure(modelBuilder);

            modelBuilder.ApplyConfiguration(new AssesmentConfiguration());
            modelBuilder.ApplyConfiguration(new FieldAssesmentGroupConfiguration());
            modelBuilder.ApplyConfiguration(new FieldConfiguration());
            modelBuilder.ApplyConfiguration(new FieldDataConfiguration());
            modelBuilder.ApplyConfiguration(new FieldTypeConfiguration());

            //FormTemplate.Configure(modelBuilder);
            //FormTemplateStatus.Configure(modelBuilder);
            //FormTemplateFieldMap.Configure(modelBuilder);

            //User.Configure(modelBuilder);
            //UserState.Configure(modelBuilder);
            //UserRoleMap.Configure(modelBuilder);

            //Role.Configure(modelBuilder);

            //Document.Configure(modelBuilder);
            //DocumentType.Configure(modelBuilder);

            //var allEntityies = modelBuilder.Model.GetEntityTypes();
            //foreach (var entity in allEntityies) 
            //{
            //    if (IsAssignebleFrom(entity.ClrType, typeof(IUpdatebleCreateable))) 
            //    {
            //        entity.AddProperty("CreatedAt", typeof(DateTime));
            //        entity.AddProperty("LastUpdatesAt", typeof(DateTime?));
            //    }
            //}
            //FieldGroup.Configure(modelBuilder);
            //Deeplink.Configure(modelBuilder);
            //DeeplinkState.Configure(modelBuilder);

            //Team.Configure(modelBuilder);

            //Project.Configure(modelBuilder);

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
