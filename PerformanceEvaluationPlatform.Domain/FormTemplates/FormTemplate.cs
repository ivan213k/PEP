using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Domain.FormTemplates
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
    }
}
