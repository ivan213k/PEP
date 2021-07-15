using System;

namespace PerformanceEvaluationPlatform.Models.Project.RequestModels
{
    public class CreateProjectRequestModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string CoordinatorId { get; set; }
    }
}
