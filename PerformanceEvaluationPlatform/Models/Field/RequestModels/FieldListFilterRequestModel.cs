using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class FieldListFilterRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> AssesmentGroupIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public SortOrder? FieldNameSortOrder { get; set; }
    }
}