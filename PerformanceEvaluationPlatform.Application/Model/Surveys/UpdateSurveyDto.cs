using System;

namespace PerformanceEvaluationPlatform.Application.Model.Surveys
{
    public class UpdateSurveyDto
    {
        public DateTime AppointmentDate { get; set; }
        public int RecommendedLevelId { get; set; }
        public string Summary { get; set; }
    }
}
