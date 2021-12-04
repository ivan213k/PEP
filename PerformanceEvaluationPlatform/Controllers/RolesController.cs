using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Application.Services.Role;
using PerformanceEvaluationPlatform.Models.Role.RequestModels;
using PerformanceEvaluationPlatform.Models.Role.ViewModels;
using PerformanceEvaluationPlatform.Models.Shared;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService ?? throw new ArgumentNullException(nameof(rolesService));
        }

        [Route("roles")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RoleListFilterRequestModel filter)
        {
            var itemsResponse = await _rolesService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var itemsVm = new ListItemsViewModel<RoleListItemViewModel>
            {
                Items = itemsResponse.Payload.Items?.Select(t => t.AsViewModel()).ToList(),
                TotalItemsCount = itemsResponse.Payload.TotalItemsCount
            };

            return Ok(itemsVm);
        }

        [Route("roles/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetRoleDetails([FromRoute] int id)
        {
            var detailsResponse = await _rolesService.GetDetails(id);

            if (TryGetErrorResult(detailsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var detailsVm = detailsResponse.Payload.AsViewModel();
            return Ok(detailsVm);
        }

        [Route("roles/{id:int}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateRoleRequestModel requestModel)
        {
            ServiceResponse serviceResponse = await _rolesService.Update(id, requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }

        [Route("roles")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequestModel requestModel)
        {
            ServiceResponse<int> serviceResponse = await _rolesService.Create(requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };
        }
    }
}
