using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dao
{
    public class FormTemplateStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public ICollection<FormTemplate> FormTemplates { get; set; }

        public static void Configure(ModelBuilder modelBuilder)
        {
            var formTemplateStatusBuilder = modelBuilder.Entity<FormTemplateStatus>();
            formTemplateStatusBuilder.ToTable("FormTemplateStatus");
            formTemplateStatusBuilder.HasKey(s => s.Id);
            formTemplateStatusBuilder.Property(s => s.Name).IsRequired().HasMaxLength(128);

            formTemplateStatusBuilder.HasMany<FormTemplate>()
                .WithOne(t => t.FormTemplateStatus)
                .HasForeignKey(t => t.StatusId)
                .IsRequired();
        }
    }
}
