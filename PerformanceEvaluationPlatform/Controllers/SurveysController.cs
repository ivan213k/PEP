using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Application.Services.Surveys;
using PerformanceEvaluationPlatform.Models.Shared;
using PerformanceEvaluationPlatform.Models.Survey.RequestModels;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class SurveysController : BaseController
    {
        private readonly ISurveyService _surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService ?? throw new ArgumentNullException(nameof(surveyService));
        }

        [HttpGet("surveys")]
        public async Task<IActionResult> Get([FromQuery] SurveyListFilterRequestModel filter)
        {
            var response = await _surveyService.GetListItems(filter.AsDto());
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            var surveyListItemsViewModel = new ListItemsViewModel<SurveyListItemViewModel>
            {
                Items = response.Payload?.Items?.Select(t => t.AsViewModel()).ToList(),
                TotalItemsCount = response.Payload.TotalItemsCount
            };
   
            return Ok(surveyListItemsViewModel);
        }

        [HttpGet("surveys/{id}")]
        public async Task<IActionResult> GetSurveyDetails([FromRoute] int id)
        {
            var response = await _surveyService.GetDetails(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return Ok(response.Payload.AsViewModel());
        }

        [HttpPost("surveys")]
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyRequestModel surveyRequestModel)
        {
            var response = await _surveyService.Create(surveyRequestModel.AsDto());
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return CreatedAtAction(nameof(GetSurveyDetails), new { id = response.Payload }, new IdViewModel {Id = response.Payload });
        }

        [HttpPut("surveys/{id}")]
        public async Task<IActionResult> EditSurvey(int id, [FromBody] EditSurveyRequestModel surveyRequestModel)
        {
            var response = await _surveyService.Update(id, surveyRequestModel.AsDto());
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }
            return Ok();
        }

        [HttpPut("surveys/{id}/ready")]
        public async Task<IActionResult> ChangeSurveyStateToReady(int id) 
        {
            var response = await _surveyService.ChangeStateToReady(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return NoContent();
        }

        [HttpPut("surveys/{id}/sent")]
        public async Task<IActionResult> ChangeSurveyStateToSent(int id)
        {
            var response = await _surveyService.ChangeStateToSent(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return NoContent();
        }

        [HttpPut("surveys/{id}/readyForReview")]
        public async Task<IActionResult> ChangeSurveyStateToReadyForReview(int id)
        {
            var response = await _surveyService.ChangeStateToReadyForReview(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return NoContent();
        }

        [HttpPut("surveys/{id}/archived")]
        public async Task<IActionResult> ChangeSurveyStateToArchived(int id)
        {
            var response = await _surveyService.ChangeStateToArchived(id);
            if (TryGetErrorResult(response, out IActionResult errorResult))
            {
                return errorResult;
            }

            return NoContent();
        }

        [HttpGet("surveys/states")]
        public async Task<IActionResult> GetStates()
        {
            var itemsResponse = await _surveyService.GetStateListItems();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }
            var items = itemsResponse.Payload.Select(t => t.AsFilterDropDownItemViewModel());

            return Ok(items);
        }
    }
}
