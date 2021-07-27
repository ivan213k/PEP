﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Roles.Dto;
using PerformanceEvaluationPlatform.DAL.Models.User.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Roles
{
    public class RolesRepository : BaseRepository, IRolesRepository
    {

        public RolesRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<RoleListItemDto>> GetList(RoleListFilterDto filter)
        {
            var parameters = new
            {
                Skip = filter.Skip,
                Take = filter.Take,
                Search = filter.Search,
                IsPrimary = filter.IsPrimary,
                UsersCountFrom = filter.UsersCountFrom,
                UsersCountTo = filter.UsersCountTo,
                TitleSortOrder = filter.TitleSortOrder,
                IsPrimarySortOrder = filter.IsPrimarySortOrder
            };

            return ExecuteSp<RoleListItemDto>("[dbo].[spGetRoleListItems]", parameters);
        }

        public async Task<RoleDetailsDto> GetDetails(int id)
        {
            var role = await DbContext.Set<Role>()
                .Include(r => r.UserRoleMaps)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (role == null)
            {
                return null;
            }

            var details = new RoleDetailsDto
            {
                Id = role.Id,
                Title = role.Title,
                IsPrimary = role.IsPrimary,
                UsersCount = role.UserRoleMaps.Count()
            };

            return details;
        }

        public async Task<Role> Get(int roleId)
        {
            return await DbContext.Set<Role>().FirstOrDefaultAsync(s => s.Id == roleId);
        }
    }
}
