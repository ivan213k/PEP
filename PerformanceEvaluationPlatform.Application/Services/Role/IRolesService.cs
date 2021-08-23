using PerformanceEvaluationPlatform.Application.Model.Roles;
using PerformanceEvaluationPlatform.Application.Packages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Role
{
    public interface IRolesService
    {
        Task<ServiceResponse<IList<RoleListItemDto>>> GetListItems(RoleListFilterDto filter);
        Task<ServiceResponse<RoleDetailsDto>> GetDetails(int id);
        Task<ServiceResponse> Update(int id, UpdateRoleDto model);
        Task<ServiceResponse<int>> Create(CreateRoleDto model);
    }
}
