using PerformanceEvaluationPlatform.Application.Model.Users;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Users
{
    public interface IUserService
    {
        public Task<ServiceResponse<IList<UserListItemDto>>> GetList(UserFilterDto filter);
        public Task<ServiceResponse<IList<UserStateListItemDto>>> GetStates();
        public Task<ServiceResponse<IList<SystemRoleDto>>> GetSystemRoles();
        public Task<ServiceResponse<SystemRoleDto>> GetSystemRole(string id);
        public Task<ServiceResponse<UserDetailDto>> GetDetail(int id);
        public Task<ServiceResponse> Update(int id,UpdateUserDto updatedUser);
        public Task<ServiceResponse> Create(CreateUserDto userToCreate, bool IsDevelop);
        public Task<ServiceResponse> Suspend(int id);
        public Task<ServiceResponse> Activate(int id);

    }
}
