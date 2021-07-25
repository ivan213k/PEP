using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto
{
    public class DeeplinkListFilterDto
    {
        public string Search { get; set; }
        public int? SentToId { get; set; }

        public ICollection<int> StateIds { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public DateTime? ExpiresAtFrom { get; set; }
        public DateTime? ExpiresAtTo { get; set; }


        public int? ExpiresAtOrder { get; set; }
        public int? SentToOrder { get; set; }

    }
}
