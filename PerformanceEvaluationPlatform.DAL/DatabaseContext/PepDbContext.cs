using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;

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

            Survey.Configure(modelBuilder);
            SurveyState.Configure(modelBuilder);
            Level.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
