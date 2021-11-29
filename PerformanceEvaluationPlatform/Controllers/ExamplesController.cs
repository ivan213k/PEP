using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Example;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class ExamplesController : BaseController
    {
        private readonly IExamplesService _examplesService;


        public ExamplesController(IExamplesService examplesService)
        {
	        _examplesService = examplesService ?? throw new ArgumentNullException(nameof(examplesService));
        }

        [HttpGet("examples")]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery]ExampleListFilterRequestModel filter)
        {
            var itemsResponse = await _examplesService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult)) {
	            return errorResult;
            }

            var itemsVm = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(itemsVm);
        }

        [HttpGet("examples/types")]
        [Authorize]
        public async Task<IActionResult> GetTypes()
        {
	        var itemsResponse = await _examplesService.GetTypeListItems();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
	        {
		        return errorResult;
	        }

            var items = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(items);
        }

        [HttpGet("examples/states")]
        [Authorize]
        public async Task<IActionResult> GetStates()
        {
	        var itemsResponse = await _examplesService.GetStateListItems();
	        if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
	        {
		        return errorResult;
	        }

            var items = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(items);
        }

        [HttpGet("examples/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
	        var detailsResponse = await _examplesService.GetDetails(id);
	        if (TryGetErrorResult(detailsResponse, out IActionResult errorResult))
	        {
		        return errorResult;
	        }

            var detailsVm = detailsResponse.Payload.AsViewModel();
            return Ok(detailsVm);
        }

        [HttpPut("examples/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UpdateExampleRequestModel requestModel) {

	        ServiceResponse serviceResponse = await _examplesService.Update(id, requestModel.AsDto());
	        return TryGetErrorResult(serviceResponse, out IActionResult errorResult) 
		        ? errorResult 
		        : Ok();
        }

        [HttpPost("examples")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]CreateExampleRequestModel requestModel)
        {

            ServiceResponse<int> serviceResponse = await _examplesService.Create(requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
	            ? errorResult
	            : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };
        }
    }
}
