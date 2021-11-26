using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Application.Interfaces.Projects;
using PerformanceEvaluationPlatform.Application.Model.Projects;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Domain.Projects;
using PerformanceEvaluationPlatform.Persistence.DatabaseContext;
using PerformanceEvaluationPlatform.Persistence.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Persistence.Repositories.Projects
{
    public class ProjectsRepository : BaseRepository, IProjectsRepository
    {
        public ProjectsRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext) : base(databaseOptions, dbContext)
        {
        }

        public Task<ListItemsDto<ProjectListItemDto>> GetList(ProjectListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                CoordinatorIds = filter.CoordinatorIds,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = filter.TitleSortOrder,
                StartDateSortOrder = filter.StartDateSortOrder,
                CoordinatorSortOrder = filter.CoordinatorSortOrder
            };
     
            return ExecuteMultiResultSetSp<ProjectListItemDto>("[dbo].[spGetProjectListItems]", parameters);
        }

        public Task<Project> GetById(int id)
        {
            return Get<Project>(id);
        }

        public Task Create(Project project)
        {
            return Create<Project>(project);
        }
    }
}
