namespace PerformanceEvaluationPlatform.Models.Field.ViewModels
{
    public class FieldDetailsViewModel
    {   
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssesmentGroupName { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; }

    }
}
