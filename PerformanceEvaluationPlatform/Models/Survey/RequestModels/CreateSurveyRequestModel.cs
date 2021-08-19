using PerformanceEvaluationPlatform.Application.Model.Surveys;
using PerformanceEvaluationPlatform.Models.Shared.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Survey.RequestModels
{
    public class CreateSurveyRequestModel
    {
        [Required]
        public int FormId { get; set; }

        [Required]
        public int AssigneeId { get; set; }

        [Required]
        [GreaterThanNow(ErrorMessage = "Back date entry not allowed")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public int RecommendedLevelId { get; set; }

        [Required]
        public int SupervisorId { get; set; }

        [Required]
        public ICollection<int> AssignedUserIds { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static CreateSurveyDto AsDto(this CreateSurveyRequestModel requestModel)
        {
            return new CreateSurveyDto
            {
                AppointmentDate = requestModel.AppointmentDate,
                SupervisorId = requestModel.SupervisorId,
                RecommendedLevelId = requestModel.RecommendedLevelId,
                FormId = requestModel.FormId,
                AssigneeId = requestModel.AssigneeId,
                AssignedUserIds = requestModel.AssignedUserIds
            };
        }
    }
}
