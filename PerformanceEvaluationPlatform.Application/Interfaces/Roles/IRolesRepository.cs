 using PerformanceEvaluationPlatform.Application.Model.Roles;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Domain.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Roles
{
    public interface IRolesRepository : IBaseRepository
    {
        Task<ListItemsDto<RoleListItemDto>> GetList(RoleListFilterDto filter);
        Task<RoleDetailsDto> GetDetails(int id);
        public Task<Role> Get(int roleId);
        public Task<bool> IsTittleNotUnique(string title, int? id = null);
        Task Create(Role role);
    }
}
