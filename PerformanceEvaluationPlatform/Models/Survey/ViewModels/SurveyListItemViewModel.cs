using PerformanceEvaluationPlatform.Application.Model.Surveys;
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
        public double ProgressInPercenteges { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static SurveyListItemViewModel AsViewModel(this SurveyListItemDto dto)
        {
            return new SurveyListItemViewModel
            {
                Id = dto.Id,
                AppointmentDate = dto.AppointmentDate,
                Assignee = $"{dto.AssigneeFirstName} {dto.AssigneeLastName}",
                AssigneeId = dto.AssigneeId,
                Supervisor = $"{dto.SupervisorFirstName} {dto.SupervisorLastName}",
                SupervisorId = dto.SupervisorId,
                FormName = dto.FormName,
                FormId = dto.FormId,
                State = dto.StateName,
                StateId = dto.StateId,
                ProgressInPercenteges = dto.ProgressInPercenteges
            };
        }
    }
}
