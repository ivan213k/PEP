using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class CreateFormTemplateRequestModel
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        public IList<FormTemplateFieldRequestModel> Fields { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static CreateFormTemplateDto AsDto(this CreateFormTemplateRequestModel requestModel)
        {
            return new CreateFormTemplateDto
            {
                Name = requestModel.Name,
                Fields = requestModel.Fields.Select(f => f.AsDto()).ToList()
            };
        }
    }
   
}
