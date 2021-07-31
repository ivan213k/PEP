using PerformanceEvaluationPlatform.Models.FormTemplates.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class UpdateFormTemplateRequestModel: IFormTemplateRequest
    {
        [Required]
        public IList<FormTemplateFieldRequestModel> Fields { get; set; }
    }

   
}
