using PerformanceEvaluationPlatform.Application.Model.FormsData;
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

    public static partial class ViewModelMapperExtensions
    {
        public static FormDataListItemViewModel AsViewModel(this FormDataListItemDto dto)
        {
            return new FormDataListItemViewModel
            {
                Id = dto.Id,
                AssigneeId = dto.AssigneeId,
                Assignee = $"{dto.AssigneeFirstName} {dto.AssigneeLastName}",
                AppointmentDate = dto.AppointmentDate,
                FormId = dto.FormId,
                FormName = dto.FormName,
                Reviewer = $"{dto.ReviewerFirstName} {dto.ReviewerLastName}",
                ReviewerId = dto.ReviewerId,
                State = dto.StateName,
                StateId = dto.StateId
            };
        }
    }
}
