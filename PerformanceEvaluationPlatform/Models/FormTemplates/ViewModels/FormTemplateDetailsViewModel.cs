using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public static partial class ViewModelMapperExtensions
    {
        public static FormTemplateDetailsViewModel AsViewModel(this FormTemplateDetailsDto dto)
        {
            return new FormTemplateDetailsViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Version = dto.Version,
                CreatedAt = dto.CreatedAt,
                StatusId = dto.FormTemplateStatusId,
                Status = dto.Status,
                Fields = dto.Fields?
                .Select(t => new FormTemplateFieldViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Order = t.Order,
                    FieldTypeId = t.FieldTypeId,
                    FieldTypeName = t.FieldTypeName
                }).ToList()
            };
        }
    }
}
