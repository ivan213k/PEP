using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao
{
    public class DeeplinkState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Deeplink> Deeplinks { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var exampleTypeBuilder = modelBuilder.Entity<DeeplinkState>();
            exampleTypeBuilder.ToTable("DeeplinkState");
            exampleTypeBuilder.HasKey(t => t.Id);
            exampleTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);

            exampleTypeBuilder.HasMany<Deeplink>().WithOne(t => t.DeeplinkState)
                .HasForeignKey(t => t.StateId).IsRequired();
            
        }
    }
}
