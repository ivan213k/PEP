using System;

namespace PerformanceEvaluationPlatform.Application.Model.Projects
{
    public class ProjectDetailsDto
    {
        public string Title { get; set; }
        public DateTime StartDate { set; get; }
        public int CoordinatorId { get; set; }
    }
}
