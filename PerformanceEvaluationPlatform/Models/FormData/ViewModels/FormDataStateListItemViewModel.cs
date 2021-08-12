using PerformanceEvaluationPlatform.Application.Model.FormsData;

namespace PerformanceEvaluationPlatform.Models.FormData.ViewModels
{
    public class FormDataStateListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static FormDataStateListItemViewModel AsViewModel(this FormDataStateListItemDto dto)
        {
            return new FormDataStateListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
