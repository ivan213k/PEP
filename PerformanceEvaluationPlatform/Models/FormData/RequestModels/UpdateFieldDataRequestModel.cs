using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.FormData.RequestModels
{
    public class UpdateFieldDataRequestModel
    {
        [Required]
        public int FieldId { get; set; }
        [Required]
        public int AssesmentId { get; set; }
        public string Comment { get; set; }
    }
}
