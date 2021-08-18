using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.FormTemplates.Interfaces
{
    public interface IFormTemplateRequest
    {
        public IList<FormTemplateFieldDto> Fields { get; set; }
    }
}
