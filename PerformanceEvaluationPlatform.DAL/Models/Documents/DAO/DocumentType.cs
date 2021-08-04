using Microsoft.EntityFrameworkCore;

namespace PerformanceEvaluationPlatform.DAL.Models.Documents.Dao
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var documentTypeBuilder = modelBuilder.Entity<DocumentType>();
            documentTypeBuilder.ToTable(nameof(DocumentType));
            documentTypeBuilder.HasKey(t => t.Id);
            documentTypeBuilder.Property(t => t.Name).IsRequired().HasMaxLength(128);

        }
    }
}
