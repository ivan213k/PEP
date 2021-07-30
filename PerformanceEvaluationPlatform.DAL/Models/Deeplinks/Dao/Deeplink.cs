using System;
using Microsoft.EntityFrameworkCore;
using  PerformanceEvaluationPlatform.DAL.Models.Users.Dao;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao
{
    public class Deeplink
    {
        public int Id { get; set; }

        public Guid Code { get; set; }

        public int UserId { get; set; }

        public DateTime ExpireDate { get; set; }
       
        public int StateId { get; set; }
       
        public DeeplinkState DeeplinkState { get; set; }

        public User User { get; set; }

        public int SurveyId { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var deeplinkTypeBuilder = modelBuilder.Entity<Deeplink>();
            deeplinkTypeBuilder.ToTable(nameof(Deeplink));
            deeplinkTypeBuilder.HasKey(t => t.Id);
            deeplinkTypeBuilder.Property(t => t.ExpireDate).IsRequired();
            deeplinkTypeBuilder.Property(t => t.UserId).IsRequired();
            deeplinkTypeBuilder.Property(t => t.StateId).IsRequired();

            deeplinkTypeBuilder.HasOne(t => t.DeeplinkState)
                .WithMany()
                .HasForeignKey(t => t.StateId)
                .IsRequired();

            deeplinkTypeBuilder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();
        }

    }
}
