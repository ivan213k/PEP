using PerformanceEvaluationPlatform.Application.Model.FormsData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataDetailViewModel
    {
        public string FormName { get; set; }
        public int FormId { get; set; }
        public string Assignee { get; set; }
        public int AssigneeId { get; set; }
        public string Reviewer { get; set; }
        public int ReviewerId { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string RecommendedLevel { get; set; }
        public int RecommendedLevelId { get; set; }
        public string Project { get; set; }
        public int ProjectId { get; set; }
        public string Team { get; set; }
        public int TeamId { get; set; }
        public int ExperienceInCompany { get; set; }
        public string EnglishLevel { get; set; }
        public string Period { get; set; }
        public string CurrentPosition { get; set; }
        public ICollection<FormDataAnswersItemViewModel> Answers { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static FormDataDetailViewModel AsViewModel(this FormDataDetailsDto dto)
        {
            return new FormDataDetailViewModel
            {
                FormId = dto.FormId,
                FormName = dto.FormName,
                Assignee = dto.Assignee,
                AssigneeId = dto.AssigneeId,
                Reviewer = dto.Reviewer,
                ReviewerId = dto.ReviewerId,
                AppointmentDate = dto.AppointmentDate,
                RecommendedLevel = dto.RecommendedLevel,
                RecommendedLevelId = dto.RecommendedLevelId,
                State = dto.State,
                StateId = dto.FormDataStateId,
                Project = dto.Project,
                ProjectId = dto.ProjectId,
                Team = dto.Team,
                TeamId = dto.TeamId,
                Period = dto.Period,
                ExperienceInCompany = dto.ExperienceInCompany,
                EnglishLevel = dto.EnglishLevel,
                CurrentPosition = dto.CurrentPosition,
                Answers = dto.Answers?
                .Select(fd => new FormDataAnswersItemViewModel
                {
                    Assessment = fd.Assessment,
                    Comment = fd.Comment,
                    TypeId = fd.TypeId,
                    TypeName = fd.TypeName,
                    Order = fd.Order,
                })
                .OrderBy(fd => fd.Order)
                .ToList()
            };
        }
    }
}
