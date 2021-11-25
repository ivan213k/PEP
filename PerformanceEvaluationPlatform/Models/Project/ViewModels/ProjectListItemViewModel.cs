using PerformanceEvaluationPlatform.Application.Model.Projects;
using System;

namespace PerformanceEvaluationPlatform.Models.Project.ViewModels
{
    public class ProjectListItemViewModel
    {
        public int Id { get; set;}
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string Coordinator { get; set; }
        public int CoordinatorId { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static ProjectListItemViewModel AsViewModel(this ProjectListItemDto dto)
        {
            return new ProjectListItemViewModel
            {
                Id = dto.Id,
                Coordinator = $"{dto.CoordinatorFirstName} {dto.CoordinatorLastName}",
                CoordinatorId = dto.CoordinatorId,
                Title = dto.Title,
                StartDate = dto.StartDate,
            };
        }
    }
}