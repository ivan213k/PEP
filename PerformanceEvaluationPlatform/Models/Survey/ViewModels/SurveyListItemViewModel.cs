using System;

namespace PerformanceEvaluationPlatform.Models.Survey.ViewModels
{
    public class SurveyListItemViewModel
    {
        public int Id { get; set; }
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Supervisor { get; set; }
        public int SupervisorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
    }
}
