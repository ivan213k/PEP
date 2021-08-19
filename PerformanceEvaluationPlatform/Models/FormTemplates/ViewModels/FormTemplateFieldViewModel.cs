namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplateFieldViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int FieldTypeId { get; set; }
        public string FieldTypeName { get; set; }
    }
}
