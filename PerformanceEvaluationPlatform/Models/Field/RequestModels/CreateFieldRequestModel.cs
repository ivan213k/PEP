namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class CreateFieldRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssesmentGroupName { get; set; }
        public int TypeId { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }
        public string FieldGroupName { get; set; }
        public int FieldGroupId { get; set; }
        public string Description { get; set; }
    }
}