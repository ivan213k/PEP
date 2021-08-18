using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using System;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplateListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static FormTemplateListItemViewModel AsViewModel(this FormTemplateListItemDto dto)
        {
            return new FormTemplateListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Version = dto.Version,
                StatusName = dto.StatusName,
                StatusId = dto.StatusId,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}
