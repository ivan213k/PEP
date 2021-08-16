using System;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.Documents
{
    public class DocumentListFilterDto
    {
        public ICollection<int> UserIds { get; set; }
        public ICollection<int> TypeIds { get; set; }
        public DateTime? ValidTo { get; set; }
        public int? NameSortOrder { get; set; }
        public int? TypeSortOrder { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public string Search { get; set; }
    }
}
