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
        public Task<UserDetailDto> GetUser(int id);
        public Task UpdateUser(List<int> roleIds,int userId);
        public Task<bool> UserEmailValidation(string email, int id);
        public Task<User> GetUserValidation(int id);
        public Task<List<User>> GetUsersValidation(ICollection<int> userIds);
        public Task Save();

    }
}
