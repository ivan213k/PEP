using System;

namespace PerformanceEvaluationPlatform.Application.Model.Projects
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int CoordinatorId { get; set; }
    }
}
