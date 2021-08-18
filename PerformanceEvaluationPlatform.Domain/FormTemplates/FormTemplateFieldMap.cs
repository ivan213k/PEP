using PerformanceEvaluationPlatform.Domain.Fields;

namespace PerformanceEvaluationPlatform.Domain.FormTemplates
{
    public class FormTemplateFieldMap
    {
        public int Id { get; set; }
        public int FormTemplateId { get; set; }
        public int FieldId { get; set; }
        public int Order { get; set; }

        public Field Field { get; set; }
    }
}
