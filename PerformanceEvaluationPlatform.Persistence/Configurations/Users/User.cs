using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerformanceEvaluationPlatform.Domain.Users;

namespace PerformanceEvaluationPlatform.Persistence.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(s => s.Id);
            builder.Property(fn => fn.FirstName)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(ln => ln.LastName)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(fdi => fdi.FirstDayInIndustry)
                .IsRequired();

            builder.Property(fdi => fdi.FirstDayInCompany)
               .IsRequired();

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(40);

            builder.HasOne(us => us.UserState)
                .WithMany()
                .HasForeignKey(s => s.StateId)
                .IsRequired();
        }
    }
}
