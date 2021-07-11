using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class EditFieldRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsRequired { get; set; }
    }
}