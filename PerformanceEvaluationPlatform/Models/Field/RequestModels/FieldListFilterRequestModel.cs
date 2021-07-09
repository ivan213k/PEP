using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class FieldListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string Search { get; set; }
        public ICollection<int> AssesmentGroupIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public SortOrder? FieldNameSortOrder { get; set; }
    }
}