using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.User.Dao;
using PerformanceEvaluationPlatform.DAL.Models.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Users
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        public UserRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            :base(databaseOptions,dbContext)
        {
        }


        public async Task<ICollection<UserListItemDto>> GetUsers(UserFilterDto filter)
        {
            var paramaters = new
            {
                Search = filter.Search,
                StateIds = filter.StateIds,
                RoleIds = filter.RoleIds,
                PreviousPeDate = filter.PreviousPeDate,
                NextPeDate = filter.NextPeDate,
                UserNameSort = filter.UserNameSort,
                UserPreviousPE = filter.UserPreviousPE,
                UserNextPE = filter.UserNextPE,
                Skip = filter.Skip,
                Take = filter.Take
            };
            return  await ExecuteSp<UserListItemDto>("[dbo].[spGetUserListItems]", paramaters);
        }

        public async Task<ICollection<UserStateListItemDto>> GetUserStates()
        {
            var userStates = await DbContext.Set<UserState>().Select(s => new UserStateListItemDto() { Id = s.Id, Name = s.Name }).ToListAsync(); ;

            return userStates;
        }
       
    }
}
