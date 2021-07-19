using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Models.FieldGroups.RequestModels
{
    public class FieldGroupsListFilterRequestModel : BaseFilterRequestModel
    {
        public bool IsNotEmptyOnly { get; set; }
        public int? CountFrom { get; set; }
        public int? CountTo { get; set; }

        public SortOrder? TitleSetOrder { get; set; }
        public SortOrder? FieldCountSetOrder { get; set; }
    }
}
