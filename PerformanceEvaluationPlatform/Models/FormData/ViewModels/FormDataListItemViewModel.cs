using PerformanceEvaluationPlatform.Models.FormData.Enums;
using System;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataListItemViewModel
    {
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public StateEnum State { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
