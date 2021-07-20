using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Team.RequestModels
{
    public class TeamListFilterRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> ProjectIds { get; set; }
        public SortOrder? OrderByTeamTitle { get; set; }
        public SortOrder? OrderByProjectTitle { get; set; }
        public SortOrder? OrderByTeamSize { get; set; }
    }
}
