using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Example.RequestModels
{
    public class ExampleListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public int? StateId { get; set; }
        public ICollection<int> TypeIds { get; set; }
    }
}
