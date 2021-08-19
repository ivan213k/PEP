using PerformanceEvaluationPlatform.Application.Model.FormTemplates.Dto;

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
    }
}
