using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Team.RequestModels
{
    public class TeamListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public ICollection<int> ProjectIds { get; set; }
        public SortOrder? OrderByTeamTitle { get; set; }
        public SortOrder? OrderByProjectTitle { get; set; }
        public SortOrder? OrderByTeamSize { get; set; }
    }
}
