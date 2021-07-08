using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Projects.RequestModels
{
    public class ProjectListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public ICollection<int> CoordinatorIds { get; set; }
    }
}