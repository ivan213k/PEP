﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Users.Dto;

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
        public async Task<ICollection<UserListItemDto>> GetList(UserFilterDto filter)
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


        public async Task<UserDetailDto> GetDetail(int id)
        {
            var user =await DbContext.Set<User>()
                .Include(s => s.TechnicalLevel)
                .Include(s => s.EnglishLevel)
                .Include(s => s.Team)
                .Include(s => s.UserState)
                .Include(s=>s.Roles).ThenInclude(s=>s.Role)
                .Include(s=>s.Surveys)
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
                Role = user.Roles.Select(s => s.Role.Title).ToList(),
                State = user.UserState.Name,
                TechnicalLevel = user.TechnicalLevel.Name,
                EnglishLevel = user.EnglishLevel.Name,
                YearsInCompany = DateTime.Now.Year - user.FirstDayInCompany.Year,
                YearsOfExpirience = DateTime.Now.Year - user.FirstDayInIndustry.Year,
                NextPeDate = user.Surveys.Select(s => s.AppointmentDate).FirstOrDefault(s => s > DateTime.Now),
                PreviousPEDate = user.Surveys.Select(s => s.AppointmentDate).OrderBy(s => s).Skip(1).Take(1).FirstOrDefault(),
                PreviousPes = user.Surveys.Select(s => s.AppointmentDate).Where(s=>s<DateTime.Now).ToList()
            };
            return userDetailDto;
        }
        public async Task<ICollection<UserStateListItemDto>> GetStates()
        {
            var userStates = await DbContext.Set<UserState>().Select(s => new UserStateListItemDto() { Id = s.Id, Name = s.Name }).ToListAsync(); ;

            return userStates;
        }

        public Task<User> Get(int id)
        {
            return Get<User>(id);
        }

        public Task<List<User>> GetList(ICollection<int> userIds)
        {
            return DbContext.Set<User>().Where(r => userIds.Contains(r.Id)).ToListAsync();
        }


        public async Task<User> Get(string email)
        {
            var userValidation = await DbContext.Set<User>().FirstOrDefaultAsync(s => s.Email.Trim().ToLower() == email.ToLower().Trim());
            return userValidation;
        }

        public async Task Update(List<int> roleIds,int id)
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

        public Task Create(User user)
        {
            return Create<User>(user);
        }
        
        public Task Save()
        {
            return SaveChanges();
        }

        public async Task<ICollection<SystemRoleDto>> GetSystemRoles()
        {
            return await DbContext.Set<SystemRole>().Select(s => new SystemRoleDto() 
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        public async Task<SystemRoleDto> GetSystemRole(string id)
        {
            return await DbContext.Set<SystemRole>().Select(s => new SystemRoleDto()
            {
                Id = s.Id,
                Name = s.Name
            }).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
