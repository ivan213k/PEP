using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Projects;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Projects
{
    public class ProjectsRepository : BaseRepository, IProjectsRepository
    {
        public ProjectsRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }

        public Task<IList<ProjectListItemDto>> GetList(ProjectListFilterDto filter)
        {
            var parameters = new
            {
                Search = filter.Search,
                CoordinatorId = filter.CoordinatorId,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = filter.TitleSortOrder,
                StartDateSortOrder = filter.StartDateSortOrder,
                CoordinatorSortOrder = filter.CoordinatorSortOrder
            };

            return ExecuteSp<ProjectListItemDto>("[dbo].[spGetProjectListItems]", parameters);
        }
    }
}
