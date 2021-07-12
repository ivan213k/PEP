using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Survey.RequestModels
{
    public class CreateSurveyRequestModel
    {
        public int FormId { get; set; }
        public int AssigneeId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int RecommendedLevelId { get; set; }
        public int SupervisorId { get; set; }
        public ICollection<int> AssignedUserIds { get; set; }
    }
}
