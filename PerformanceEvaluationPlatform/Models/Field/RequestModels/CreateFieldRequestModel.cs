using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class CreateFieldRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
}