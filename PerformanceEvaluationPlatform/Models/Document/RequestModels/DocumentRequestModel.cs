using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class DocumentRequestModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
        public ICollection<int> UserIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public DateTime? ValidTo { get; set; }
        public SortOrder? SortOrder { get; set; }
        public SortCategy? SortCategy { get; set; }
    }
}
