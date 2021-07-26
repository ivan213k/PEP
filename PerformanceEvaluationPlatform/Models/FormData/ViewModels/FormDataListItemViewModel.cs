using System;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataListItemViewModel
    {
        public int Id { get; set; }
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
