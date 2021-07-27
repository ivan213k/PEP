using PerformanceEvaluationPlatform.DAL.Models.Roles.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Roles
{
    public interface IRolesRepository
    {
        Task<IList<RoleListItemDto>> GetList(RoleListFilterDto filter);
        Task<RoleDetailsDto> GetDetails(int id);
        public Task<Role> Get(int roleId);
    }
}
