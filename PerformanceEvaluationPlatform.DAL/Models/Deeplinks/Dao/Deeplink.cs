using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao
{
    public class Deeplink
    {
        public int Id { get; set; }
        
        public int SentToId { get; set; }

        public DateTime ExpiresAt { get; set; }
       
        public int StateId { get; set; }
        public int FormTemplateId { get; set; }
       
        public DeeplinkState DeeplinkState { get; set; }


        public static void Configure(ModelBuilder modelBuilder)
        {
            var DeeplinkTypeBuilder = modelBuilder.Entity<Deeplink>();
            DeeplinkTypeBuilder.ToTable(nameof(Deeplink));
            DeeplinkTypeBuilder.HasKey(t => t.Id);
            DeeplinkTypeBuilder.Property(t => t.ExpiresAt).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.FormTemplateId).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.SentToId).IsRequired();
            DeeplinkTypeBuilder.Property(t => t.StateId).IsRequired();


            DeeplinkTypeBuilder.HasOne(t => t.DeeplinkState)
                .WithMany()
                .HasForeignKey(t => t.StateId)
                .IsRequired();

        }

    }
}
