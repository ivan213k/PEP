using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao;

namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dao
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FieldTypeId { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }
        public FieldAssesmentGroup AssesmentGroup { get; set; }
        public FieldType FieldType { get; set; }
        public int? FieldGroupId { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var fieldBuilder = modelBuilder.Entity<Field>();
            fieldBuilder.ToTable("Field");
            fieldBuilder.HasKey(t => t.Id);
            fieldBuilder.Property(t => t.Name).IsRequired().HasMaxLength(256);
            fieldBuilder.Property(t => t.FieldTypeId).IsRequired();
            fieldBuilder.Property(t => t.AssesmentGroupId).IsRequired();
            fieldBuilder.Property(t => t.Description).IsRequired().HasMaxLength(256);

            fieldBuilder.HasOne<FieldType>(t => t.FieldType)
                .WithMany()
                .HasForeignKey(t => t.FieldTypeId)
                .IsRequired();

            fieldBuilder.HasOne<FieldAssesmentGroup>(t => t.AssesmentGroup)
                .WithMany()
                .HasForeignKey(t => t.AssesmentGroupId)
                .IsRequired();

            fieldBuilder.HasMany<FormTemplateFieldMap>()
                .WithOne(m=>m.Field)
                .HasForeignKey(m => m.FieldId)
                .IsRequired();

        }
    }
}
