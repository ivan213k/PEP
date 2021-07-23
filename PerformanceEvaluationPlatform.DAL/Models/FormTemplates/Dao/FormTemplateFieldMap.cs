using Microsoft.EntityFrameworkCore;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao
{
    public class FormTemplateFieldMap
    {
        public int Id { get; set; }
        public int FormTemplateId { get; set; }
        public int FieldId { get; set; }
        public int Order { get; set; }

        public Field Field { get; set; }

        public static void Configure (ModelBuilder modelBuilder)
        {
            var formTemplateFieldMapBuilder = modelBuilder.Entity<FormTemplateFieldMap>();
            formTemplateFieldMapBuilder.ToTable("FormTemplateFieldMap");
            formTemplateFieldMapBuilder.HasKey(m => m.Id);
            formTemplateFieldMapBuilder.Property(m => m.FormTemplateId).IsRequired();
            formTemplateFieldMapBuilder.Property(m => m.FieldId).IsRequired();
            formTemplateFieldMapBuilder.Property(m => m.Order).IsRequired();

            formTemplateFieldMapBuilder.HasOne<FormTemplate>()
                .WithMany(t=>t.FormTemplateFieldMaps)
                .HasForeignKey(m => m.FormTemplateId)
                .IsRequired();

            formTemplateFieldMapBuilder.HasOne<Field>(m=>m.Field)
                .WithMany()
                .HasForeignKey(m => m.FieldId)
                .IsRequired();
        }
    }
}
