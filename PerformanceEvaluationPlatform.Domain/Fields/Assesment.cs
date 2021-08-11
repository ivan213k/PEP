
namespace PerformanceEvaluationPlatform.Domain.Fields
{
    public class Assesment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsCommentRequired { get; set; }

        public FieldAssesmentGroup AssesmentGroup { get; set; }

    }
}
