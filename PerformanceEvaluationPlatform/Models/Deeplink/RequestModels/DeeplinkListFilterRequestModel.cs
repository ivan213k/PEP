using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Deeplink.RequestModels
{
    public class DeeplinkListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public int? SentToId { get; set; }
        //need Exprices at from
        //need Exprices at to

       // public int? StateIds { get; set; }
        public ICollection<int> StateIds { get; set; }
    }
}
