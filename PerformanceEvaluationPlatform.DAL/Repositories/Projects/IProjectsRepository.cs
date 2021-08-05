using PerformanceEvaluationPlatform.DAL.Models.Projects;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Projects
{
    public interface IProjectsRepository : IBaseRepository
    {
        Task<IList<ProjectListItemDto>> GetList(ProjectListFilterDto filter);
        Task<Project> Get(int projectId);
        Task Create(Project project);
    }
}
