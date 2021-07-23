using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.FormTemplates.Dto
{
    public class FormTemplateFieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int FieldTypeId { get; set; }
        public string FieldTypeName { get; set; }
    }
}
