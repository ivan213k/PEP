using PerformanceEvaluationPlatform.Application.Interfaces.Projects;
using PerformanceEvaluationPlatform.Application.Model.Projects;
using PerformanceEvaluationPlatform.Application.Model.Shared;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Projects
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public async Task<ServiceResponse<int>> Create(CreateProjectDto createProjectDto)
        {
            if (createProjectDto is null)
            {
                return ServiceResponse<int>.BadRequest();
            }
            Project project = new Project
            {
                Title = createProjectDto.Title,
                StartDate = createProjectDto.StartDate,
                CoordinatorId = createProjectDto.CoordinatorId
            };

            await _projectsRepository.Create(project);
            await _projectsRepository.SaveChanges();

            return ServiceResponse<int>.Success(project.Id, 201);
        }

        public async Task<ServiceResponse<ProjectDetailsDto>> GetById(int projectId)
        {
            var project = await _projectsRepository.GetById(projectId);
            if (project is null)
            {
                return ServiceResponse<ProjectDetailsDto>.NotFound();
            }
            var detailsDto = new ProjectDetailsDto
            {
                Id = project.Id,
                StartDate = project.StartDate,
                Title = project.Title,
                CoordinatorId = project.CoordinatorId, 
            };
            return ServiceResponse<ProjectDetailsDto>.Success(detailsDto);
        }

        public async Task<ServiceResponse<ListItemsDto<ProjectListItemDto>>> GetList(ProjectListFilterDto filter)
        {
            var projects = await _projectsRepository.GetList(filter);
            return ServiceResponse<ListItemsDto<ProjectListItemDto>>.Success(projects);
        }

        public async Task<ServiceResponse> Update(int id, UpdateProjectDto updateProjectDto)
        {
            if (updateProjectDto is null)
            {
                return ServiceResponse.BadRequest();
            }
            var project = await _projectsRepository.GetById(id);
            if (project is null)
            {
                return ServiceResponse.NotFound();
            }
            project.Title = updateProjectDto.Title;
            project.StartDate = updateProjectDto.StartDate;
            project.CoordinatorId = updateProjectDto.CoordinatorId;

            await _projectsRepository.SaveChanges();
            return ServiceResponse.Success();
        }
    }
}
