using PerformanceEvaluationPlatform.Domain.Users;
using System;

namespace PerformanceEvaluationPlatform.Domain.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int CoordinatorId { get; set; }
        public User Coordinator { get; set; }
    }
}
