using System.ComponentModel.DataAnnotations;
using PerformanceEvaluationPlatform.Application.Model.Fields;

namespace PerformanceEvaluationPlatform.Models.Field.RequestModels
{
    public class EditFieldRequestModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int AssesmentGroupId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

    }
    public static partial class ViewModelMapperExtensions
    {
        public static EditFieldDto AsDto(this EditFieldRequestModel requestModel)
        {
            return new EditFieldDto
            {
                Name = requestModel.Name,
                TypeId = requestModel.TypeId,
                AssesmentGroupId = requestModel.AssesmentGroupId,
                Description = requestModel.Description 
            };
        }
    }
}

