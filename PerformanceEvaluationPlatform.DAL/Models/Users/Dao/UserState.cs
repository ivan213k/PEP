using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Users.Dao
{
    public class UserState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var userStateTableBuilder = modelBuilder.Entity<UserState>();
            userStateTableBuilder.ToTable("UserState");

            userStateTableBuilder.HasKey(s => s.Id);

            userStateTableBuilder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(60);

        }
    }
}
