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


        public async Task<UserDetailDto> GetUser(int id)
        {
            var user =await DbContext.Set<User>()
                .Include(s => s.TechnicalLevel)
                .Include(s => s.EnglishLevel)
                .Include(s => s.Team)
                .Include(s => s.UserState)
                .Include(s=>s.Roles).ThenInclude(s=>s.Role)
                .SingleOrDefaultAsync(s=>s.Id == id);

            if(user is null)
            {
                return null;
            }

            var userDetailDto = new UserDetailDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                FirstDayInCompany = user.FirstDayInCompany,
                Team = user.Team.Title,
                Project = user.Team.ProjectId.ToString(),
                Role = user.Roles.Select(s=>s.Role.Title).ToList(),
                State = user.UserState.Name,
                TechnicalLevel = user.TechnicalLevel.Name,
                EnglishLevel = user.EnglishLevel.Name,
                YearsInCompany = DateTime.Now.Year - user.FirstDayInCompany.Year,
                YearsOfExpirience = DateTime.Now.Year - user.FirstDayInIndustry.Year,
                NextPeDate = new DateTime(2001, 01, 01),
                PreviousPEDate = new DateTime(2001, 01, 01),
                PreviousPes = new DateTime[] { new DateTime(2001,01,01) }
            };
            return userDetailDto;
        }
        public async Task<ICollection<UserStateListItemDto>> GetUserStates()
        {
            var userStates = await DbContext.Set<UserState>().Select(s => new UserStateListItemDto() { Id = s.Id, Name = s.Name }).ToListAsync(); ;

            return userStates;
        }

        public Task<User> GetUserValidation(int id)
        {
            return Get<User>(id);
        }

        public Task<List<User>> GetUsersValidation(ICollection<int> userIds)
        {
            return DbContext.Set<User>().Where(r => userIds.Contains(r.Id)).ToListAsync();
        }


        public async Task<bool> UserEmailValidation(string email, int id)
        {
            var userValidation = await DbContext.Set<User>().FirstOrDefaultAsync(s => s.Email.Trim().ToLower() == email.ToLower().Trim()
            && s.Id != id);
            if(userValidation != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateUser(List<int> roleIds,int id)
        {
            var roleToDelteFromUser = DbContext.Set<UserRoleMap>().Where(s => s.UserId == id);
            DbContext.Set<UserRoleMap>().RemoveRange(roleToDelteFromUser);
            List<UserRoleMap> roleUserToCreate = new List<UserRoleMap>();
            for (int i = 0; i < roleIds.Count; i++)
            {
                roleUserToCreate.Add(new UserRoleMap { RoleId = roleIds[i], UserId = id });
            }
            await DbContext.Set<UserRoleMap>().AddRangeAsync(roleUserToCreate);
        }

        public Task Save()
        {
            return SaveChanges();
        }
    }
}
