using PerformanceEvaluationPlatform.Application.Model.Roles;

namespace PerformanceEvaluationPlatform.Models.Role.ViewModels
{
    public class RoleListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrimary { get; set; }
        public int UsersCount { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static RoleListItemViewModel AsViewModel(this RoleListItemDto dto)
        {
            return new RoleListItemViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                IsPrimary = dto.IsPrimary,
                UsersCount = dto.UsersCount
            };
        }
    }
}
