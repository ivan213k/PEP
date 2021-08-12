using System;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Application.Services.FormsData;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormsDataController : BaseController
    {
        private readonly IFormDataService _formDataService;

        public FormsDataController(IFormDataService formDataService)
        {
            _formDataService = formDataService ?? throw new ArgumentNullException(nameof(formDataService));
        }

        [HttpGet("forms")]
        public async Task<IActionResult> Get([FromQuery] FormDataListFilterRequestModel filter)
        {
            var formDataResponse = await _formDataService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(formDataResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var itemsVm = formDataResponse.Payload.Select(t => t.AsViewModel());
            return Ok(itemsVm);
        }

        [HttpGet("forms/states")]
        public async Task<IActionResult> GetStates()
        {
            var formDataResponse = await _formDataService.GetStateListItems();
            if (TryGetErrorResult(formDataResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var items = formDataResponse.Payload.Select(t => t.AsViewModel());
            return Ok(items);
        }

        [HttpGet("forms/{id:int}")]
        public async Task<IActionResult> GetDetailsPage([FromRoute] int id)
        {
            var detailsResponse = await _formDataService.GetDetails(id);
            if (TryGetErrorResult(detailsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var detailsVm = detailsResponse.Payload.AsViewModel();
            return Ok(detailsVm);
        }

        [HttpPut("forms/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] IList<UpdateFieldDataRequestModel> requestModel)
        {
            ServiceResponse serviceResponse = await _formDataService.Update(id, requestModel.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)
                ? errorResult
                : Ok();
        }
    }
}
