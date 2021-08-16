using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Deeplinks;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Deeplinks
{
    public class DeeplinkConfiguration : IEntityTypeConfiguration<Deeplink>
    {
        public void Configure(EntityTypeBuilder<Deeplink> deeplinkBuilder)
        {
            deeplinkBuilder.ToTable("Deeplink");
            deeplinkBuilder.HasKey(t => t.Id);
            deeplinkBuilder.Property(t => t.ExpireDate).IsRequired();
            deeplinkBuilder.Property(t => t.SurveyId).IsRequired();
            deeplinkBuilder.Property(t => t.UserId).IsRequired();
            deeplinkBuilder.Property(t => t.StateId).IsRequired();
            deeplinkBuilder.Property(t => t.Code).IsRequired();
            deeplinkBuilder.Property(t => t.SentAt).IsRequired(false);
            deeplinkBuilder.Property(t => t.SentById).IsRequired(false);

            deeplinkBuilder.HasOne(t => t.DeeplinkState)
                .WithMany(t => t.Deeplinks)
                .HasForeignKey(t => t.StateId)
                .IsRequired();
            // wait User and Survey
           /* deeplinkBuilder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            deeplinkBuilder.HasOne(t => t.SentBy)
                .WithMany()
                .HasForeignKey(t => t.SentById)
                .IsRequired();

            deeplinkBuilder.HasOne(t => t.Survey)
               .WithMany(t => t.DeepLinks)
                .HasForeignKey(t => t.SurveyId)
                .IsRequired();*/
        }

    }
}
