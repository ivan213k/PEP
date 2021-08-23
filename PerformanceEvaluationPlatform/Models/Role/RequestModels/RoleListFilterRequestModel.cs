using PerformanceEvaluationPlatform.Application.Model.Roles;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Models.Role.RequestModels
{
    public class RoleListFilterRequestModel : BaseFilterRequestModel
    {
        public bool? IsPrimary { get; set; }
        public int? UsersCountFrom { get; set; }
        public int? UsersCountTo { get; set; }

        public SortOrder? TitleSortOrder { get; set; }
        public SortOrder? IsPrimarySortOrder { get; set; }
    }

    public static partial class ViewModelMapperExtensions
    {
        public static RoleListFilterDto AsDto(this RoleListFilterRequestModel viewmodel)
        {
            return new RoleListFilterDto
            {
                Search = viewmodel.Search,
                Skip = viewmodel.Skip,
                Take = viewmodel.Take,
                IsPrimary = viewmodel.IsPrimary,
                UsersCountFrom = viewmodel.UsersCountFrom,
                UsersCountTo = viewmodel.UsersCountTo,
                TitleSortOrder = (int?)viewmodel.TitleSortOrder,
                IsPrimarySortOrder = (int?)viewmodel.IsPrimarySortOrder
            };
        }
    }
}
