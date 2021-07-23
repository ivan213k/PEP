using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto
{
    public class FormTemplateDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public ICollection<FormTemplateFieldDto> Fields { get; set; }
    }
}
