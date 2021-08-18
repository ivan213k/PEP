using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class UpdateFormTemplateRequestModel
    {
        [Required]
        public IList<FormTemplateFieldRequestModel> Fields { get; set; }
    }
}
