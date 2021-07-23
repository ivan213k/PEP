using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao
{
    public class FormTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }

        public FormTemplateStatus FormTemplateStatus { get; set; }
        public ICollection<FormTemplateFieldMap> FormTemplateFieldMaps { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var formTemplateBuilder = modelBuilder.Entity<FormTemplate>();
            formTemplateBuilder.ToTable("FormTemplate");
            formTemplateBuilder.HasKey(t => t.Id);
            formTemplateBuilder.Property(t => t.Name).HasMaxLength(128).IsRequired();
            formTemplateBuilder.Property(t => t.Version).IsRequired();
            formTemplateBuilder.Property(t => t.CreatedAt).IsRequired();
            formTemplateBuilder.Property(t => t.StatusId).IsRequired();

            formTemplateBuilder.HasOne<FormTemplateStatus>()
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .IsRequired();

            formTemplateBuilder.HasMany<FormTemplateFieldMap>()
                .WithOne()
                .HasForeignKey(m => m.FormTemplateId)
                .IsRequired();
        }



    }
}
