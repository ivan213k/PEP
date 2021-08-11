using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.ViewModels
{
    public class FieldDetailsViewModel
    {   
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssesmentGroupName { get; set; }
        public bool IsRequired { get; set; }
        public string Description { get; set; }

    }
    public static partial class ViewModelMapperExtensions
    {
        public static FieldDetailsViewModel AsViewModel(this FieldDetailsDto dto)
        {
            return new FieldDetailsViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                AssesmentGroupName = dto.AssasmentGroupName,
                Type = dto.TypeName,
                IsRequired = dto.IsRequired,
                Description = dto.Description
            };
        }
    }
}
