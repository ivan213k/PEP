using System;

namespace PerformanceEvaluationPlatform.Application.Model.Projects
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { set; get; }
        public int CoordinatorId { get; set; }
    }
}
