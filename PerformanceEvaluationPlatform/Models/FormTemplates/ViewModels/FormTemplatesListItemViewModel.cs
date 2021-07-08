using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplatesListItemViewModel
    {
        public string Name { get; set; }

        public string Version { get; set; }
        // can be enum
        public string Status { get; set; }
        // change to class
        public string AssesmentGroup { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
