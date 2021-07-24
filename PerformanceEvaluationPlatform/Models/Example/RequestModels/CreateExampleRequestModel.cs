using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Example.RequestModels
{
    public class CreateExampleRequestModel
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int StateId { get; set; }
    }
}
