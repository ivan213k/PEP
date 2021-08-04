using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using System;
using System.Collections.Generic;
using FormDataDao = PerformanceEvaluationPlatform.DAL.Models.FormData.Dao.FormData;

namespace PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao
{
    public class Survey
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int FormTemplateId { get; set; }
        public int AssigneeId { get; set; }
        public int SupervisorId { get; set; }
        public int RecommendedLevelId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Summary { get; set; }

        public SurveyState SurveyState { get; set; }
        public Level RecomendedLevel { get; set; }

        public User Asignee { get; set; }
        public User Supervisor { get; set; }
        public FormTemplate FormTemplate { get; set; }

        public ICollection<Deeplink> DeepLinks { get; set; }
        public ICollection<FormDataDao> FormData { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var surveyTypeBuilder = modelBuilder.Entity<Survey>();
            surveyTypeBuilder.ToTable(nameof(Survey));
            surveyTypeBuilder.HasKey(t => t.Id);
            surveyTypeBuilder.Property(t => t.AppointmentDate).IsRequired();
            surveyTypeBuilder.Property(t => t.AssigneeId).IsRequired();
            surveyTypeBuilder.Property(t => t.SupervisorId).IsRequired();
            surveyTypeBuilder.Property(t => t.FormTemplateId).IsRequired();
            surveyTypeBuilder.Property(t => t.StateId).IsRequired();
            surveyTypeBuilder.Property(t => t.RecommendedLevelId).IsRequired();

            surveyTypeBuilder.HasOne(t => t.SurveyState)
                .WithMany()
                .HasForeignKey(t => t.StateId)
                .IsRequired();

            surveyTypeBuilder.HasOne(t => t.RecomendedLevel)
                .WithMany()
                .HasForeignKey(t => t.RecommendedLevelId)
                .IsRequired();

            surveyTypeBuilder.HasMany(t=>t.DeepLinks)
                .WithOne()
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();

            surveyTypeBuilder.HasOne(t => t.Asignee)
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .IsRequired();

            surveyTypeBuilder.HasOne(t => t.Supervisor)
                .WithMany()
                .HasForeignKey(t => t.SupervisorId)
                .IsRequired();

            surveyTypeBuilder.HasOne(t => t.FormTemplate)
                .WithMany()
                .HasForeignKey(t => t.FormTemplateId)
                .IsRequired();
            surveyTypeBuilder.HasMany(t => t.FormData)
                .WithOne()
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();
        }
    }
}
