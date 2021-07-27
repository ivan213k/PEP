using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Projects.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Projects;
using PerformanceEvaluationPlatform.Models.Project.RequestModels;
using PerformanceEvaluationPlatform.Models.Project.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

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
                CoordinatorId = filter.CoordinatorIds
            };

            var itemsDto = await _projectRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new ProjectListItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                StartDate = t.StartDate,
                Coordinator = t.Coordinator
            });

            return Ok(items);
        }

        private IEnumerable<ProjectListItemViewModel> GetSortedItems(IEnumerable<ProjectListItemViewModel> projects, ProjectListFilterRequestModel filter)
        {
            if (filter.TitleSortOrder != null)
            {
                if (filter.TitleSortOrder == SortOrder.Ascending)
                    projects = projects.OrderBy(r => r.Title);
                else
                    projects = projects.OrderByDescending(r => r.Title);
            }
            if (filter.CoordinatorSortOrder != null)
            {
                if (filter.CoordinatorSortOrder == SortOrder.Ascending)
                    projects = projects.OrderBy(r => r.Coordinator);
                else
                    projects = projects.OrderByDescending(r => r.Coordinator);
            }
            if (filter.StartDateSortOrder != null)
            {
                if (filter.StartDateSortOrder == SortOrder.Ascending)
                {
                    projects = projects.OrderBy(r => r.StartDate);
                }
                else
                { 
                    projects = projects.OrderByDescending(r => r.StartDate);
                }
            }
            return projects;
        }

        [HttpPost("projects")]
        public IActionResult CreateProject([FromBody] CreateProjectRequestModel projectRequestModel)
        {
            if (projectRequestModel is null)
            {
                return BadRequest();
            }
            return Created("projects", projectRequestModel);
        }

        [HttpPut("project/{id}")]
        public IActionResult EditProject(int id, [FromBody] EditProjectRequestModel projectRequestModel)
        {
            if (projectRequestModel is null)
            {
                return BadRequest();
            }
            var projects = GetProjectListItemViewModels();
            var projectExists = projects.Any(t => t.Id == id);
            if (!projectExists)
            {
                return NotFound();
            }

            return Ok();
        }

        private IEnumerable<ProjectListItemViewModel> GetProjectListItemViewModels()
        {
            var items = new List<ProjectListItemViewModel>
            {
                new ProjectListItemViewModel
                {
                    Title = "Project 1",
                    StartDate = new DateTime(2021,7,10),
                    Coordinator = "H1",
                    CoordinatorId = 1
                },
                new ProjectListItemViewModel
                {
                    Title = "Project 2",
                    StartDate = new DateTime(2021,7,1),
                    Coordinator = "H2",
                    CoordinatorId = 2
                },
                new ProjectListItemViewModel
                {
                    Title = "Project 3",
                    StartDate = new DateTime(2021,7,5),
                    Coordinator = "H3",
                    CoordinatorId = 3
                },
            };
            return items;
        }
    }
}
