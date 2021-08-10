using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Teams.Dto
{
    public class TeamListFilterDto
    {
        public ICollection<int> ProjectIds { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public int? TitleSortOrder { get; set; }

        public int? ProjectTitleSortOrder { get; set; }

        public int? TeamSizeSortOrder { get; set; }
    }
}
