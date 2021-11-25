using PerformanceEvaluationPlatform.Application.Model.Projects;
using System;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Project.RequestModels
{
    public class EditProjectRequestModel
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int CoordinatorId { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static UpdateProjectDto AsDto(this EditProjectRequestModel requestModel)
        {
            return new UpdateProjectDto
            {
                CoordinatorId = requestModel.CoordinatorId,
                Title = requestModel.Title,
                StartDate = requestModel.StartDate,
            };
        }
    }
}
