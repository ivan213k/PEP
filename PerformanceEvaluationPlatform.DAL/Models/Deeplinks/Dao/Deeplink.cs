using Microsoft.EntityFrameworkCore;
using System;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao
{
    public class Deeplink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid Code { get; set; }
        public int StateId { get; set; }
        public int? SentById { get; set; }
        public User SentBy { get; set; }
        public DateTime? SentAt { get; set; }
        public DeeplinkState DeeplinkState { get; set; }
        public User User { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }



        public static void Configure(ModelBuilder modelBuilder)
        {
            var deeplinkTypeBuilder = modelBuilder.Entity<Deeplink>();
            deeplinkTypeBuilder.ToTable("Deeplink");
            deeplinkTypeBuilder.HasKey(t => t.Id);
            deeplinkTypeBuilder.Property(t => t.ExpireDate).IsRequired();
            deeplinkTypeBuilder.Property(t => t.SurveyId).IsRequired();
            deeplinkTypeBuilder.Property(t => t.UserId).IsRequired();
            deeplinkTypeBuilder.Property(t => t.StateId).IsRequired();
            deeplinkTypeBuilder.Property(t => t.Code).IsRequired();
            deeplinkTypeBuilder.Property(t => t.SentAt).IsRequired(false);
            deeplinkTypeBuilder.Property(t => t.SentById).IsRequired(false);

            deeplinkTypeBuilder.HasOne(t => t.DeeplinkState)
                .WithMany(t => t.Deeplinks)
                .HasForeignKey(t => t.StateId)
                .IsRequired();

            deeplinkTypeBuilder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            deeplinkTypeBuilder.HasOne(t => t.SentBy)
                .WithMany()
                .HasForeignKey(t => t.SentById)
                .IsRequired();

            deeplinkTypeBuilder.HasOne(t => t.Survey)
               .WithMany(t => t.DeepLinks)
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();
        }

    }
}
