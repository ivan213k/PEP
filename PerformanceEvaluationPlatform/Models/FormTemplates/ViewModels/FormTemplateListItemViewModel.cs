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
        
        public string Status { get; set; }

        public int StatusId { get; set; }
        
        public string AssesmentGroup { get; set; }

        public int AssesmentGroupId { get; set; } 

        public DateTime CreatedAt { get; set; }
    }
}
