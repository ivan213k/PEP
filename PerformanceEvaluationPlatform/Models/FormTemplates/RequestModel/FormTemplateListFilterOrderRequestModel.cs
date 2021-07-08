using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class FormTemplateListFilterOrderRequestModel
    {
        public string Search { get; set; }
        public ICollection<int> StatusIds { get; set; }
        public ICollection<int> AssesmentGroupIds { get; set; }
        public string Sort { get; set; }
    }
}
