using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.ViewModels
{
    public class FieldTypeListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static FieldTypeListItemViewModel AsViewModel(this FieldTypeListItemDto dto)
        {
            return new FieldTypeListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
