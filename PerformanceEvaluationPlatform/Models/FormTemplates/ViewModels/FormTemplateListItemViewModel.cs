using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplateListItemViewModel
    {
        public string Name { get; set; }

        public int Version { get; set; }
        // can be enum
        public string Status { get; set; }

        public int StatusId { get; set; }
        // change to class
        public string AssesmentGroup { get; set; }

        public int AssesmentGroupId { get; set; } 

        public DateTime CreatedAt { get; set; }
    }
}
