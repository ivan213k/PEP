using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Application.Model.Fields
{
    public class EditFieldDto
    {
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public int TypeId { get; set; }
        public int AssesmentGroupId { get; set; }
        public string Description { get; set; }

    }
}