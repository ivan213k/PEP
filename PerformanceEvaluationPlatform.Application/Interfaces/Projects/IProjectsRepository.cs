using PerformanceEvaluationPlatform.Application.Model.Projects;
using PerformanceEvaluationPlatform.Domain.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Projects
{
    public interface IProjectsRepository : IBaseRepository
    {
        Task<IList<ProjectListItemDto>> GetList(ProjectListFilterDto filter);
        Task<Project> GetById(int projectId);
        Task Create(Project project);
    }
}
