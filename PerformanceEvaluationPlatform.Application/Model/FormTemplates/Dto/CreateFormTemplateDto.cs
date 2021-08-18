using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Interfaces;
using System.Collections.Generic;

namespace PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto
{
    public class CreateFormTemplateDto: IFormTemplateRequest
    {
        public string Name { get; set; }
        public IList<FormTemplateFieldDto> Fields { get; set; }
    }
}
