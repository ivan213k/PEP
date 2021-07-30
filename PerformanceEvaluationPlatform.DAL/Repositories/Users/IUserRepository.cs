using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<ICollection<UserListItemDto>> GetList(UserFilterDto filter);
        public Task<ICollection<UserStateListItemDto>>GetStates();
        public Task<UserDetailDto> GetDetail(int id);
        public Task Update(List<int> roleIds,int userId);
        public Task<User> Get(string email);
        public Task<User> Get(int id);
        public Task<List<User>> GetList(ICollection<int> userIds);
        public Task Create(User user);
        public Task Save();

    }
}
