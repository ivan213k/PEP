using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.ViewModels
{
    public class FieldListItemViewModel
    {   
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssesmentGroupName { get; set; }
        public int TypeId { get; set; }
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }

    }
    public static partial class ViewModelMapperExtensions
    {
        public static FieldListItemViewModel AsViewModel(this FieldListItemDto dto)
        {
            return new FieldListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                AssesmentGroupName = dto.AssesmentGroup,
                Type = dto.Type,
                IsRequired = dto.IsRequired
            };
        }
    }
}
