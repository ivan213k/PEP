using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;
using PerformanceEvaluationPlatform.Models.Survey.RequestModels;
using PerformanceEvaluationPlatform.Models.Survey.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveysRepository _surveysRepository;

        public SurveysController(ISurveysRepository surveysRepository)
        {
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
        }

        [HttpGet("surveys")]
        public async Task<IActionResult> Get([FromQuery] SurveyListFilterRequestModel filter)
        {
            var filterDto = new SurveyListFilterDto
            {
                AppointmentDateFrom = filter.AppointmentDateFrom,
                AppointmentDateTo = filter.AppointmentDateTo,
                AssigneeSortOrder = (int?)filter.AssigneeSortOrder,
                FormNameSortOrder = (int?)filter.FormNameSortOrder,
                Search = filter.Search,
                StateIds = filter.StateIds,
                AssigneeIds = filter.AssigneeIds,
                SupervisorIds = filter.SupervisorIds,
                Skip = filter.Skip,
                Take = filter.Take
            };
            var surveysDto = await _surveysRepository.GetList(filterDto);

            var surveys = surveysDto.Select(t => new SurveyListItemViewModel
            {
                Id = t.Id,
                AppointmentDate = t.AppointmentDate,
                Assignee = $"{t.AssigneeFirstName} {t.AssigneeLastName}",
                AssigneeId = t.AssigneeId,
                Supervisor = $"{t.SupervisorFirstName} {t.SupervisorLastName}",
                SupervisorId = t.SupervisorId,
                FormName = t.FormName,
                FormId = t.FormId,
                State = t.StateName,
                StateId = t.StateId,
            });
            return Ok(surveys);
        }

        [HttpGet("surveys/{id}")]
        public async Task<IActionResult> GetSurveyDetails([FromRoute] int id)
        {
            var detailsDto = await _surveysRepository.GetDetails(id);
            if (detailsDto == null)
            {
                return NotFound();
            }

            var detailsViewModel = new SurveyDetailsViewModel
            {
                AppointmentDate = detailsDto.AppointmentDate,
                Assignee = detailsDto.Assignee,
                AssigneeId = detailsDto.AssigneeId,
                FormId = detailsDto.FormId,
                FormName = detailsDto.FormName,
                RecommendedLevel = detailsDto.RecommendedLevel,
                RecommendedLevelId = detailsDto.RecommendedLevelId,
                State = detailsDto.State,
                StateId = detailsDto.StateId,
                Supervisor = detailsDto.Supervisor,
                SupervisorId = detailsDto.SupervisorId,
                Summary = detailsDto.Summary,
                AssignedUsers = detailsDto.AssignedUsers?
                    .Select(t => new SurveyAssigneeViewModel
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToList()
            };

            return Ok(detailsViewModel);
        }
     
        [HttpPost("surveys")]
        public IActionResult CreateSurvey([FromBody] CreateSurveyRequestModel surveyRequestModel)
        {
            if (surveyRequestModel is null)
            {
                return BadRequest();
            }
            return Created("surveys", surveyRequestModel);
        }

        [HttpPut("surveys/{id}")]
        public IActionResult EditSurvey(int id, [FromBody] EditSurveyRequestModel surveyRequestModel)
        {
            if (surveyRequestModel is null)
            {
                return BadRequest();
            }
            var surveys = GetSurveyListItemViewModels();
            var survey = surveys.Where(r => r.Id == id).SingleOrDefault();
            if (survey is null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("surveys/states")]
        public async Task<IActionResult> GetStates()
        {
            var itemsDto = await _surveysRepository.GetStatesList();
            var items = itemsDto
                .Select(t => new SurveyStateListItemViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return Ok(items);
        }

        private IEnumerable<SurveyListItemViewModel> GetSurveyListItemViewModels()
        {
            var items = new List<SurveyListItemViewModel>
            {
                new SurveyListItemViewModel
                {
                    Id = 1,
                    AppointmentDate = new DateTime(2021,7,10),
                    Assignee = "Test User",
                    AssigneeId = 1,
                    Supervisor = "Admin User",
                    SupervisorId = 1,
                    FormName = "Manual QA",
                    FormId = 1,
                    State = "Active",
                    StateId = 1
                },
                new SurveyListItemViewModel
                {
                    Id = 2,
                    AppointmentDate = new DateTime(2021,7,11),
                    Assignee = "Test User 1",
                    AssigneeId = 2,
                    Supervisor = "Admin User 1",
                    SupervisorId = 2,
                    FormName = ".NET",
                    FormId = 2,
                    State = "Blocked",
                    StateId = 2
                },
                new SurveyListItemViewModel
                {
                    Id = 3,
                    AppointmentDate = new DateTime(2021,7,12),
                    Assignee = "Test User 2",
                    AssigneeId = 3,
                    Supervisor = "Admin User 2",
                    SupervisorId = 3,
                    FormName = "JS",
                    FormId = 3,
                    State = "Active",
                    StateId = 1
                },
            };
            return items;
        }
    }
}
