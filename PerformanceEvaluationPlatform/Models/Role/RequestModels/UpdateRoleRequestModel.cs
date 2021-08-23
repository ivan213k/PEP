using PerformanceEvaluationPlatform.Application.Model.Roles;
using System.ComponentModel.DataAnnotations;

namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class UpdateRoleRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Title should be greater than 1")]
        public string Title { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static UpdateRoleDto AsDto(this UpdateRoleRequestModel requestModel)
        {
            return new UpdateRoleDto
            {
                Id = requestModel.Id,
                Title = requestModel.Title,
                IsPrimary = requestModel.IsPrimary
            };
        }
    }
}
