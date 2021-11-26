using PerformanceEvaluationPlatform.Application.Model.Projects;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Domain.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Projects
{
    public interface IProjectsRepository : IBaseRepository
    {
        Task<ListItemsDto<ProjectListItemDto>> GetList(ProjectListFilterDto filter);
        Task<Project> GetById(int projectId);
        Task Create(Project project);
    }
}
