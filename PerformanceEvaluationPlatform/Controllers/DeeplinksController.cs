using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Deeplinks;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Models.Deeplink.RequestModels;
using PerformanceEvaluationPlatform.Models.Deeplink.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.Models.Shared;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class DeeplinksController : BaseController
    {
        private readonly IDeeplinksService _deeplinksService;

        public DeeplinksController(IDeeplinksService deeplinksService)
        {
            _deeplinksService = deeplinksService ?? throw new ArgumentNullException(nameof(deeplinksService));
        }

        [HttpGet("deeplinks")]
        public async Task<IActionResult> Get([FromQuery] DeeplinkListFilterRequestModel filter)
        {
            var itemsResponse = await _deeplinksService.GetList(filter.AsDto());
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var itemsVm = new ListItemsViewModel<DeeplinkListItemViewModel>
            {
                Items = itemsResponse.Payload.Items?.Select(t=> t.AsViewModel()).ToList(),
                TotalItemsCount =  itemsResponse.Payload.TotalItemsCount
            };
            return Ok(itemsVm);
        }

        [HttpGet("deeplinks/states")]
        public async Task<IActionResult> GetStates()
        {
            var itemsResponse = await _deeplinksService.GetStatesList();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var items = itemsResponse.Payload.Select(t => t.AsDropDownViewModel());
            return Ok(items);
        }

        [HttpGet("deeplinks/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var detailsResponse = await _deeplinksService.GetDetails(id);
            if (TryGetErrorResult(detailsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var detailsVm = detailsResponse.Payload.AsViewModel();
            return Ok(detailsVm);
        }

        [HttpPut("deeplinks/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDeeplinkRequestModel requestModel)
        {
            ServiceResponse serviceResponse = await _deeplinksService.Update(id, requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }

        [HttpPost("deeplinks")]
        public async Task<IActionResult> Create([FromBody] CreateDeeplinkRequestModel requestModel)
        {
            ServiceResponse<int> serviceResponse = await _deeplinksService.Create(requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };

        }
    }
}
