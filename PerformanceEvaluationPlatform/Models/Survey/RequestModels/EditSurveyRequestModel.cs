using PerformanceEvaluationPlatform.Models.Shared.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Survey.RequestModels
{
    public class EditSurveyRequestModel
    {
        [Required]
        [GreaterThanNow(ErrorMessage = "Back date entry not allowed")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public int RecommendedLevelId { get; set; }
        public string Summary { get; set; }
    }
}
