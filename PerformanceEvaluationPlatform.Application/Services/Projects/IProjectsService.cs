using PerformanceEvaluationPlatform.Application.Model.Projects;
using PerformanceEvaluationPlatform.Application.Packages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Projects
{
    public interface IProjectsService
    {
        Task<ServiceResponse<IList<ProjectListItemDto>>> GetList(ProjectListFilterDto filter);
        Task<ServiceResponse<ProjectDetailsDto>> GetById(int projectId);
        Task<ServiceResponse<int>> Create(CreateProjectDto createProjectDto);
        Task<ServiceResponse> Update(int id, UpdateProjectDto updateProjectDto);
    }
}
