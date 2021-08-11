using System.ComponentModel.DataAnnotations;
using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class CreateFieldRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int AssesmentGroupId { get; set; }
        public bool IsRequired { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
    public static partial class ViewModelMapperExtensions
    {
        public static CreateFieldDto AsDto(this CreateFieldRequestModel requestModel)
        {
            return new CreateFieldDto
            {
                Name = requestModel.Name,
                TypeId = requestModel.TypeId,
                AssesmentGroupId = requestModel.AssesmentGroupId,
                IsRequired = requestModel.IsRequired,
                Description = requestModel.Description
            };
        }
    }
}