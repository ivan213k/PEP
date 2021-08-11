namespace PerformanceEvaluationPlatform.Application.Model.Fields
{
    public class FieldDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string AssasmentGroupName { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
    }
}
