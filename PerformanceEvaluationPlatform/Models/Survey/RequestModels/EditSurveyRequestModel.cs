using System;

namespace PerformanceEvaluationPlatform.Models.Survey.RequestModels
{
    public class EditSurveyRequestModel
    {
        public DateTime AppointmentDate { get; set; }
        public int RecommendedLevelId { get; set; }
        public string Summary { get; set; }
    }
}
