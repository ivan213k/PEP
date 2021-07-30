namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dto
{
    public class FieldListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssesmentGroup { get; set; }
        public bool IsRequired { get; set; }
    }
}
