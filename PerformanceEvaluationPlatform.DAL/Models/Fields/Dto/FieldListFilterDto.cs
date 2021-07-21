using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.DAL.Models.Fields.Dto
{
    public class FieldListFilterDto
    {
        public ICollection<int> TypeIds { get; set; }
        public ICollection<int> AssesmentGroupIds { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }

        public string Search { get; set; }
        public int? TitleSortOrder { get; set; }
    }
}
