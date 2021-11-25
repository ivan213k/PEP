using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Projects;
using PerformanceEvaluationPlatform.Models.Project.RequestModels;
using PerformanceEvaluationPlatform.Models.Project.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository _projectRepository;

        public ProjectsController(IProjectsRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> Get([FromQuery] ProjectListFilterRequestModel filter)
        {
            var filterDto = new ProjectListFilterDto
            {
                Search = filter.Search,
                Skip = filter.Skip,
                Take = filter.Take,
                TitleSortOrder = (int?)filter.TitleSortOrder,
                StartDateSortOrder = (int?)filter.StartDateSortOrder,
                CoordinatorSortOrder = (int?)filter.CoordinatorSortOrder,
                CoordinatorIds = filter.CoordinatorIds
            };

            var itemsDto = await _projectRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new ProjectListItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                StartDate = t.StartDate,
                Coordinator = $"{t.CoordinatorFirstName} {t.CoordinatorLastName}",
                CoordinatorId = t.CoordinatorId,
            });

            return Ok(items);
        }

        [HttpPost("projects")]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequestModel requestModel)
        {
            var project = new Project
            {
                Title = requestModel.Title,
                StartDate = requestModel.StartDate,
                CoordinatorId = requestModel.CoordinatorId
            };

            await _projectRepository.Create(project);
            await _projectRepository.SaveChanges();

            return CreatedAtAction(nameof(project), new { id = project.Id });
        }

        [HttpPut("project/{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditProjectRequestModel requestModel)
        {
            var entity = await _projectRepository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Title = requestModel.Title;
            entity.StartDate = requestModel.StartDate;
            entity.CoordinatorId = requestModel.CoordinatorId;

            await _projectRepository.SaveChanges();
            return Ok();
        }
    }
}
