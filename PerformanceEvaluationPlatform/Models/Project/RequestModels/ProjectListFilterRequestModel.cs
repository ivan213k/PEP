using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Project.RequestModels
{
    public class ProjectListFilterRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> CoordinatorIds { get; set; }

        public SortOrder? TitleSortOrder { get; set; }
        public SortOrder? StartDateSortOrder { get; set; }
        public SortOrder? CoordinatorSortOrder { get; set; }
    }
}