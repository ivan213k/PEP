using PerformanceEvaluationPlatform.Application.Model.Users;
using PerformanceEvaluationPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Users
{
    public interface IUserRepository
    {
        public Task<IList<UserListItemDto>> GetList(UserFilterDto filter);
        public Task<IList<UserStateListItemDto>>GetStates();
        public Task<IList<SystemRoleDto>> GetSystemRoles();
        public Task<SystemRoleDto> GetSystemRole(string id);
        public Task<UserDetailDto> GetDetail(int id);
        public Task Update(List<int> roleIds,int userId);
        public Task<User> Get(string email);
        public Task<User> Get(int id);
        public Task<List<User>> GetList(ICollection<int> userIds);
        public Task Create(User user);
        public Task Save();

    }
}
