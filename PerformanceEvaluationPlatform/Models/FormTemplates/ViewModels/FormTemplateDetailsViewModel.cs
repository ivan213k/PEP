using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplateDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<FormTemplateFieldViewModel>Fields { get; set; }
    }
}
