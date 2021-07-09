using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class RoleListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public bool? IsPrimary { get; set; }
        public int? UsersCountFrom { get; set; }
        public int? UsersCountTo { get; set; }

        public SortOrder TitleSortOrder { get; set; }
        public SortOrder IsPrimarySortOrder { get; set; }
    }
}
