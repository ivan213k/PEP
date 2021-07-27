using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.User.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FieldsGroup.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Teams.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dao;

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
            ExampleType.Configure(modelBuilder);
            ExampleState.Configure(modelBuilder);
            Example.Configure(modelBuilder);
            FormData.Configure(modelBuilder);
            FormDataState.Configure(modelBuilder);

            Survey.Configure(modelBuilder);
            SurveyState.Configure(modelBuilder);
            Level.Configure(modelBuilder);

            Field.Configure(modelBuilder);
            FieldType.Configure(modelBuilder);
            FieldAssesmentGroup.Configure(modelBuilder);

            FormTemplate.Configure(modelBuilder);
            FormTemplateStatus.Configure(modelBuilder);
            FormTemplateFieldMap.Configure(modelBuilder);

            User.Configure(modelBuilder);
            UserState.Configure(modelBuilder);
            UserRoleMap.Configure(modelBuilder);

            Role.Configure(modelBuilder);

            FieldGroup.Configure(modelBuilder);
            Deeplink.Configure(modelBuilder);
            DeeplinkState.Configure(modelBuilder);

            Team.Configure(modelBuilder);

            Project.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
