using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Model.Users;
using PerformanceEvaluationPlatform.Application.Services.Projects;
using PerformanceEvaluationPlatform.Application.Services.Users;
using PerformanceEvaluationPlatform.Models.Project.RequestModels;
using PerformanceEvaluationPlatform.Models.Project.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class ProjectsController : BaseController
    {
        private const int ProjectCoordinatorRoleId = 5;
        private readonly IProjectsService _projectsService;
        private readonly IUserService _userService;

        public ProjectsController(IProjectsService projectsService, IUserService userService)
        {
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(projectsService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("projects")]
        public async Task<IActionResult> Get([FromQuery] ProjectListFilterRequestModel filter)
        {
            var response = await _projectsService.GetList(filter.AsDto());
            
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            var listItemsDto = response.Payload;
            var listItemsViewModel = new ListItemsViewModel<ProjectListItemViewModel>
            {
                TotalItemsCount = listItemsDto.TotalItemsCount,
                Items = listItemsDto.Items?.Select(t => t.AsViewModel()).ToList()
            };

            return Ok(listItemsViewModel);
        }

        [HttpPost("projects")]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequestModel requestModel)
        {
            var response = await _projectsService.Create(requestModel.AsDto());

            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return CreatedAtAction(nameof(GetProjectDetails), new { id = response.Payload }, new IdViewModel { Id = response.Payload });
        }

        [HttpGet("projects/{id}")]
        public async Task<IActionResult> GetProjectDetails([FromRoute] int id)
        {
            var response = await _projectsService.GetById(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return Ok(response.Payload.AsViewModel());
        }

        [HttpPut("projects/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditProjectRequestModel requestModel)
        {
            var response = await _projectsService.Update(id, requestModel.AsDto());

            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return Ok();
        }

        [HttpGet("projects/filterCoordinators")]
        public async Task<IActionResult> GetFilterCoordinators()
        {
            var userFilterDto = new UserFilterDto
            {
                Skip = 0,
                Take = int.MaxValue,
                RoleIds = new[] { ProjectCoordinatorRoleId }
            };
            var response = await _userService.GetList(userFilterDto);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            var filterCoordinators = response.Payload?.Select(t => new FilterDropDownItemViewModel
            {
                Id = t.Id,
                Value = $"{t.FirstName} {t.LastName}"
            });

            return Ok(filterCoordinators);
        }
    }
}
