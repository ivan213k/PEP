using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Projects.Dto
{
    public class ProjectListFilterDto
    {
        public ICollection<int> CoordinatorId { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }

        public int? TitleSortOrder { get; set; }
        public int? StartDateSortOrder { get; set; }
        public int? CoordinatorSortOrder { get; set; }
    }
}
