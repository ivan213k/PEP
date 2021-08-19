using PerformanceEvaluationPlatform.Application.Model.Surveys;
using PerformanceEvaluationPlatform.Models.Survey.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ICollection<SurveyAssigneeViewModel> AssignedUsers { get; set; }
        public string Summary { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static SurveyDetailsViewModel AsViewModel(this SurveyDetailsDto dto)
        {
            return new SurveyDetailsViewModel
            {
                AppointmentDate = dto.AppointmentDate,
                Assignee = dto.Assignee,
                AssigneeId = dto.AssigneeId,
                FormId = dto.FormId,
                FormName = dto.FormName,
                RecommendedLevel = dto.RecommendedLevel,
                RecommendedLevelId = dto.RecommendedLevelId,
                State = dto.State,
                StateId = dto.StateId,
                Supervisor = dto.Supervisor,
                SupervisorId = dto.SupervisorId,
                Summary = dto.Summary,
                AssignedUsers = dto.AssignedUsers?
                    .Select(a => new SurveyAssigneeViewModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Status = (SurveyAssignedUserStatus)(int)a.Status 
                    }).ToList()
            };
        }
    }
}
