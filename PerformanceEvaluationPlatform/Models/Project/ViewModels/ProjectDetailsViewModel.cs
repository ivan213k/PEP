using PerformanceEvaluationPlatform.Application.Model.Projects;
using System;

namespace PerformanceEvaluationPlatform.Models.Project.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { set; get; }
        public int CoordinatorId { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static ProjectDetailsViewModel AsViewModel(this ProjectDetailsDto dto)
        {
            return new ProjectDetailsViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                StartDate = dto.StartDate,
                CoordinatorId = dto.CoordinatorId,
            };
        }
    }
}
