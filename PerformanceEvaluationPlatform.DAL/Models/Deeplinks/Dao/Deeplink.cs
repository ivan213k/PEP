using System;
using Microsoft.EntityFrameworkCore;

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

        public int SurveyId { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var DeeplinkTypeBuilder = modelBuilder.Entity<Deeplink>();
            DeeplinkTypeBuilder.ToTable(nameof(Deeplink));
            DeeplinkTypeBuilder.HasKey(t => t.Id);
            DeeplinkTypeBuilder.Property(t => t.ExpireDate).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.UserId).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.StateId).IsRequired();
            DeeplinkTypeBuilder.HasOne(t => t.DeeplinkState)
                .WithMany()
                .HasForeignKey(t => t.StateId)
                .IsRequired();
        }

    }
}
