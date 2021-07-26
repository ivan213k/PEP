using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
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
        private readonly IFormTemplatesRepository _formTemplatesRepository;
        private readonly IUserRepository _userRepository;

        public SurveysController(ISurveysRepository surveysRepository, IFormTemplatesRepository formTemplatesRepository, IUserRepository userRepository)
        {
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
            _formTemplatesRepository = formTemplatesRepository ?? throw new ArgumentNullException(nameof(formTemplatesRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); 
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
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyRequestModel surveyRequestModel)
        {
            if (surveyRequestModel is null)
            {
                return BadRequest();
            }
            var formTemplate = await _formTemplatesRepository.Get(surveyRequestModel.FormId);
            if (formTemplate is null)
            {
                return BadRequest("Form template does not exists.");
            }
            var assignee = await _userRepository.GetUser(surveyRequestModel.AssigneeId);
            if (assignee is null)
            {
                return BadRequest("Assignee does not exists.");
            }
            var supervisor = await _userRepository.GetUser(surveyRequestModel.SupervisorId);
            if (supervisor is null)
            {
                return BadRequest("Supervisor does not exists.");
            }
            var level = await _surveysRepository.GetLevel(surveyRequestModel.RecommendedLevelId);
            if (level is null)
            {
                return BadRequest("Level does not exists.");
            }
            if (ContainsSameAssignedUserIds(surveyRequestModel.AssignedUserIds))
            {
                return BadRequest($"{nameof(surveyRequestModel.AssignedUserIds)} contains same user id");
            }
            var assignedUsers = await _userRepository.GetUsers(surveyRequestModel.AssignedUserIds);
            foreach (var assignedUserId in surveyRequestModel.AssignedUserIds)
            {
                var assignedUser = assignedUsers.Find(r => r.Id == assignedUserId);
                if (assignedUser is null)
                {
                    return BadRequest($"Assigned user with id = {assignedUserId}, does not exist.");
                }
            }
            var survey = new Survey
            {
                FormTemplateId = surveyRequestModel.FormId,
                AssigneeId = surveyRequestModel.AssigneeId,
                SupervisorId = surveyRequestModel.SupervisorId,
                RecommendedLevelId = surveyRequestModel.RecommendedLevelId,
                AppointmentDate = surveyRequestModel.AppointmentDate,
                StateId = GetNewSurveyStateId(),
            };

            await _surveysRepository.Create(survey);
            await _surveysRepository.SaveChanges();

            return CreatedAtAction(nameof(GetSurveyDetails), new {Id = survey.Id }, survey);
        }

        private bool ContainsSameAssignedUserIds(ICollection<int> assignedUserIds)
        {
            return assignedUserIds.Count() != assignedUserIds.Distinct().Count();
        }

        private int GetNewSurveyStateId()
        {
            return 1;
        }

        [HttpPut("surveys/{id}")]
        public async Task<IActionResult> EditSurvey(int id, [FromBody] EditSurveyRequestModel surveyRequestModel)
        {
            if (surveyRequestModel is null)
            {
                return BadRequest();
            }
            var entity = await _surveysRepository.Get(id);
            if (entity is null)
            {
                return NotFound();
            }

            var recommendedLevel = await _surveysRepository.GetLevel(surveyRequestModel.RecommendedLevelId);
            if (recommendedLevel is null)
            {
                return BadRequest("Level does not exists.");
            }

            entity.AppointmentDate = surveyRequestModel.AppointmentDate;
            entity.RecommendedLevelId = surveyRequestModel.RecommendedLevelId;
            entity.Summary = surveyRequestModel.Summary;

            await _surveysRepository.SaveChanges();
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
    }
}
