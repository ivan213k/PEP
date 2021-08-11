using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Field;
using PerformanceEvaluationPlatform.Models.Field.RequestModels;
using PerformanceEvaluationPlatform.Models.Field.ViewModels;
using PerformanceEvaluationPlatform.Application.Packages;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FieldsController : BaseController
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
        }

        [HttpPost("fields")]
        public async Task<IActionResult> Create([FromBody] CreateFieldRequestModel requestModel)
        {
            ServiceResponse<int> serviceResponse = await _fieldService.Create(requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };
        }

        [HttpPost("fields/{id:int}")]
        public async Task<IActionResult> Copy(int id)
        {
            ServiceResponse<int> serviceResponse = await _fieldService.Copy(id);
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : new ObjectResult(new { Id = serviceResponse.Payload }) { StatusCode = 201 };
        }

        [HttpPut("fields/{id:int}")]
        public async Task<IActionResult> EditField([FromRoute] int id, [FromBody] EditFieldRequestModel requestModel)
        {
            ServiceResponse serviceResponse = await _fieldService.Update(id, requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }
        [HttpDelete("fields/{id:int}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            ServiceResponse serviceResponse = await _fieldService.Delete(id);
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }

        [HttpGet("fields")]
        public async Task<IActionResult> Get([FromQuery] FieldListFilterRequestModel filter)
        {
            var itemsResponse = await _fieldService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var itemsVm = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(itemsVm);
        }

        [HttpGet("fields/{id:int}")]
        public async Task<IActionResult> GetFieldDetails(int id)
        {
            var detailsResponse = await _fieldService.GetDetails(id);
            if (TryGetErrorResult(detailsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var detailsVm = detailsResponse.Payload.AsViewModel();
            return Ok(detailsVm);
        }

        [HttpGet("fields/types")]
        public async Task<IActionResult> GetTypes()
        {
            var itemsResponse = await _fieldService.GetTypeListItems();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var items = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(items);
        }

    }
}
