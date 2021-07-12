using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Survey.ViewModels
{
    public class SurveyDetailsViewModel
    {
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Supervisor { get; set; }
        public int SupervisorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string RecommendedLevel { get; set; }
        public int RecommendedLevelId { get; set; }
        public ICollection<int> AssignedUserIds { get; set; }
        public string Summary { get; set; }
    }
}
