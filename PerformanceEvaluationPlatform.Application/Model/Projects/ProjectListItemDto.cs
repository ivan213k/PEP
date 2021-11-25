using System;

namespace PerformanceEvaluationPlatform.Application.Model.Projects
{
    public class ProjectListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { set; get; }
        public string CoordinatorFirstName { set; get; }
        public string CoordinatorLastName { get; set; }
        public int CoordinatorId { get; set; }
    }
}
