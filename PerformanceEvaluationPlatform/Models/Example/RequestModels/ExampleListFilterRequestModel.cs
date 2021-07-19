using PerformanceEvaluationPlatform.Models.Shared;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Example.RequestModels
{
    public class ExampleListFilterRequestModel : BaseFilterRequestModel
    {
        public int? StateId { get; set; }
        public ICollection<int> TypeIds { get; set; }
    }
}
