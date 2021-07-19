using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;
using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Models.Document.RequestModels
{
    public class DocumentRequestModel : BaseFilterRequestModel
    {
        public ICollection<int> UserIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public DateTime? ValidTo { get; set; }
        public SortOrder? SortOrder { get; set; }
        public SortCategy? SortCategy { get; set; }
    }
}
