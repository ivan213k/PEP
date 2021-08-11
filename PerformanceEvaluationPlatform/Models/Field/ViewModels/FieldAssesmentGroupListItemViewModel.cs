using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.ViewModels
{
    public class FieldAssesmentGroupListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static FieldAssesmentGroupListItemViewModel AsViewModel(this FieldAssesmentGroupListItemDto dto)
        {
            return new FieldAssesmentGroupListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
