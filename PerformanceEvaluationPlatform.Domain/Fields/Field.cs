
namespace PerformanceEvaluationPlatform.Domain.Fields
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
    }
}
