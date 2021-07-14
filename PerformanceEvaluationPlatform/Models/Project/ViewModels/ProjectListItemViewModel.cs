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
}