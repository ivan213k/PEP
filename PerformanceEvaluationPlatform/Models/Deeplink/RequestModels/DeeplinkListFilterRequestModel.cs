using System.Collections.Generic;
using System;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Models.Deeplink.RequestModels
{
    public class DeeplinkListFilterRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

    
        public string Search { get; set; }
        public ICollection<int> SentToIds { get; set; }

        public DateTime? ExpiresAtFrom { get; set; }
        public DateTime? ExpiresAtTo { get; set; }
        public SortOrder? OrderSentTo { get; set; }
        public SortOrder? OrderExpiresAt { get; set; }

        public ICollection<int> StateIds { get; set; }
    }
}
