using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.RequestModel
{
    public class FormTemplateFieldRequestModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int Order { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static FormTemplateFieldDto AsDto(this FormTemplateFieldRequestModel requestModel)
        {
            return new FormTemplateFieldDto
            {
                Id = requestModel.Id,
                Order = requestModel.Order
            };
        }
    }
}
