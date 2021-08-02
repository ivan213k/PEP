using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dao
{
    public class Assesment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsCommentRequired { get; set; }


        public FieldAssesmentGroup AssesmentGroup { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var assesmentsBuilder = modelBuilder.Entity<Assesment>();
            assesmentsBuilder.ToTable("Assesment");
            assesmentsBuilder.HasKey(t => t.Id);
            assesmentsBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);
            assesmentsBuilder.Property(t => t.AssesmentGroupId).IsRequired();

            assesmentsBuilder.HasOne(fd => fd.AssesmentGroup)
                .WithMany()
                .HasForeignKey(fd => fd.AssesmentGroupId)
                .IsRequired();
        }
    }
}
