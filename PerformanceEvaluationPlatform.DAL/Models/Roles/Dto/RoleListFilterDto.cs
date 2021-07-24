using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Roles.Dto
{
    public class RoleListFilterDto
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }

        public bool? IsPrimary { get; set; }
        public int? UsersCountFrom { get; set; }
        public int? UsersCountTo { get; set; }

        public int? TitleSortOrder { get; set; }
        public int? IsPrimarySortOrder { get; set; }
    }
}
