namespace PerformanceEvaluationPlatform.Application.Model.Fields
{
    public class FieldListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string AssesmentGroup { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }
    }
}
