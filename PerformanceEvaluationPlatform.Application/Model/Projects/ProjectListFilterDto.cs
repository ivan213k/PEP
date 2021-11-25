using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Projects
{
    public class ProjectListFilterDto
    {
        public ICollection<int> CoordinatorIds { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }

        public int? TitleSortOrder { get; set; }
        public int? StartDateSortOrder { get; set; }
        public int? CoordinatorSortOrder { get; set; }
    }
}
