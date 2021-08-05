using System;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Project.RequestModels
{
    public class CreateProjectRequestModel
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int CoordinatorId { get; set; }
    }
}
