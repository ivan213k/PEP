using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Teams.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Teams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Teams
{
    public class TeamsRepository : BaseRepository, ITeamsRepository
    {
        public TeamsRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) 
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<TeamListItemDto>> GetList(TeamListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                ProjectIds = filter.ProjectIds,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = filter.TitleSortOrder
            };

            return ExecuteSp<TeamListItemDto>("[dbo].[spGetTeamListItems]", parameters);
        }

        public async Task<Team> GetTeamValidation(int id)
        {
            return await Get<Team>(id);
        }
    }
}
