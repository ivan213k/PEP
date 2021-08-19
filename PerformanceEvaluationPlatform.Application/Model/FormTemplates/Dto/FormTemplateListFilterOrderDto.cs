using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto
{
    public class FormTemplateListFilterOrderDto
    {
        public ICollection<int> StatusIds { get; set; }
        public int? NameSortOrder { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Search { get; set; }
    }
}
