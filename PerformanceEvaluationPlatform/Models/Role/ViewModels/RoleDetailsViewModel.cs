using PerformanceEvaluationPlatform.Application.Model.Roles;

namespace PerformanceEvaluationPlatform.Models.Role.ViewModels
{
    public class RoleDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static RoleDetailsViewModel AsViewModel(this RoleDetailsDto dto)
        {
            return new RoleDetailsViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                IsPrimary = dto.IsPrimary,
                UsersCount = dto.UsersCount
            };
        }
    }
}
