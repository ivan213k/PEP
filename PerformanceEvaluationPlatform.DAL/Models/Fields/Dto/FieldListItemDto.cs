namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dto
{
    public class FieldListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string AssesmentGroupName { get; set; }
        public bool IsRequired { get; set; }
    }
}
