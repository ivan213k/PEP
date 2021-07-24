using PerformanceEvaluationPlatform.DAL.Models.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<ICollection<UserListItemDto>> GetUsers(UserFilterDto filter);
        public Task<ICollection<UserStateListItemDto>>GetUserStates();
        public Task<bool> DeleteUser(int id);
        public Task Save();
    }
}
