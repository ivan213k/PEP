using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;
using PerformanceEvaluationPlatform.Models.Shared;

namespace PerformanceEvaluationPlatform.Models.FormTemplates.ViewModels
{
    public class FormTemplateStatusListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static FormTemplateStatusListItemViewModel AsViewModel(this FormTemplateStatusListItemDto dto)
        {
            return new FormTemplateStatusListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
        public static FilterDropDownItemViewModel AsFilterDropDownItemViewModel(this FormTemplateStatusListItemDto dto)
        {
            return new FilterDropDownItemViewModel
            {
                Id = dto.Id,
                Value = dto.Name
            };
        }
    }
}
