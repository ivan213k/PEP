using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class FormTemplateListFilterOrderRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> StatusIds { get; set; }
        public ICollection<int> AssesmentGroupIds { get; set; }
        public SortOrder Sort { get; set; }
    }
}
