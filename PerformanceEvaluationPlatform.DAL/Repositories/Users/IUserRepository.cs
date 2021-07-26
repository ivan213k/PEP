using PerformanceEvaluationPlatform.DAL.Models.User.Dao;
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
        Task<User> GetUser(int id);
        Task<List<User>> GetUsers(ICollection<int> userIds);
        public Task<UserDetailDto> GetUser(int id);
        
    }
}
