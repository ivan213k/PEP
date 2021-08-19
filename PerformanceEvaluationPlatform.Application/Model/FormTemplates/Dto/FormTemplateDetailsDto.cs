using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto
{
    public class FormTemplateDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int FormTemplateStatusId { get; set; }
        public ICollection<FormTemplateFieldDto> Fields { get; set; }
    }
}
