using System;

namespace PerformanceEvaluationPlatform.DAL.Models.FormData.Dto
{
    public class FormDataListItemDto
    {
        public int Id { get; set; }
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeLastName { get; set; }
        public int AssigneeId { get; set; }
        public string ReviewerFirstName { get; set; }
        public string ReviewerLastName { get; set; }
        public int ReviewerId { get; set; }
        public string StateName { get; set; }
        public int StateId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
