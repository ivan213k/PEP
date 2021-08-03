using Microsoft.EntityFrameworkCore;
using System;
using  PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
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
        public int SentById { get; set; }
        public User SentBy { get; set; }
        public DateTime SentAt { get; set; }
        public DeeplinkState DeeplinkState { get; set; }
        public User User { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }



        public static void Configure(ModelBuilder modelBuilder)
        {
            var DeeplinkTypeBuilder = modelBuilder.Entity<Deeplink>();
            DeeplinkTypeBuilder.ToTable("Deeplink");
            DeeplinkTypeBuilder.HasKey(t => t.Id);
            DeeplinkTypeBuilder.Property(t => t.ExpireDate).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.SurveyId).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.UserId).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.StateId).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.SentById).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.SentAt).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.Code).IsRequired();


            DeeplinkTypeBuilder.HasOne<DeeplinkState>(t => t.DeeplinkState)
                .WithMany(t => t.Deeplinks)
                .HasForeignKey(t => t.StateId)
                .IsRequired();

            DeeplinkTypeBuilder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            DeeplinkTypeBuilder.HasOne(t => t.SentBy)
                .WithMany()
                .HasForeignKey(t => t.SentById)
                .IsRequired();

            DeeplinkTypeBuilder.HasOne(t => t.Survey)
               .WithMany(t=>t.DeepLinks)
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();
        }

    }
}
