using PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.Interfaces
{
    public interface IFormTemplateRequest
    {
        public IList<FormTemplateFieldRequestModel> Fields { get; set; }
    }
}
