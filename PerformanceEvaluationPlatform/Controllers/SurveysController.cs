using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Surveys.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
using PerformanceEvaluationPlatform.Models.Survey.Enums;
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
        private const int FormDataSubmittedStateId = 2;
        private const int DraftSurveyStateId = 1;
        private const int ReadySurveyStateId = 2;
        private const int DeepLinkStateDraftId = 1;

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

            var surveys = surveysDto.Select(s => new SurveyListItemViewModel
            {
                Id = s.Id,
                AppointmentDate = s.AppointmentDate,
                Assignee = $"{s.AssigneeFirstName} {s.AssigneeLastName}",
                AssigneeId = s.AssigneeId,
                Supervisor = $"{s.SupervisorFirstName} {s.SupervisorLastName}",
                SupervisorId = s.SupervisorId,
                FormName = s.FormName,
                FormId = s.FormId,
                State = s.StateName,
                StateId = s.StateId,
                ProgressInPercenteges = GetSurveyProgressInPercenteges(s)
            });
            return Ok(surveys);
        }

        private double GetSurveyProgressInPercenteges(SurveyListItemDto survey)
        {
            double totalScore = survey.AssignedUsers.Count;
            if (totalScore == 0)
            {
                return 0;
            }
            double score = 0;
            foreach (var assignedUser in survey.AssignedUsers)
            {
                var formDataRecord = survey.FormData
                    .SingleOrDefault(r => r != null && r.FormDataAssignedUserId == assignedUser.AssignedUserId);
                if (formDataRecord != null)
                {
                    if (formDataRecord.AssignedUserStateId == FormDataSubmittedStateId)
                        score += 1;
                    else
                        score += 0.5;
                }
            }
            return score / totalScore * 100;
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
                    .Select(a => new SurveyAssigneeViewModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Status = GetFormDataFillStatus(detailsDto.FormData, a.Id)
                    }).ToList()
            };

            return Ok(detailsViewModel);
        }

        private SurveyAssignedUserStatus GetFormDataFillStatus(ICollection<SurveyFormDataDto> formData, int assigneId)
        {
            var formDataRecord = formData.SingleOrDefault(f => f.AssignedUserId == assigneId);
    
            return formDataRecord?.StateId switch
            {
                null => SurveyAssignedUserStatus.NoData,
                FormDataSubmittedStateId => SurveyAssignedUserStatus.Done,
                _ => SurveyAssignedUserStatus.InProgress
            };
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
            var assignee = await _userRepository.Get(surveyRequestModel.AssigneeId);
            if (assignee is null)
            {
                return BadRequest("Assignee does not exists.");
            }
            var supervisor = await _userRepository.Get(surveyRequestModel.SupervisorId);
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
            var assignedUsers = await _userRepository.GetList(surveyRequestModel.AssignedUserIds);
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
                StateId = DraftSurveyStateId,
                DeepLinks = surveyRequestModel.AssignedUserIds
                .Select(userId => new Deeplink
                {
                    Code = Guid.NewGuid(),
                    UserId = userId,
                    StateId = DeepLinkStateDraftId,

                }).ToList()
            };

            await _surveysRepository.Create(survey);
            await _surveysRepository.SaveChanges();

            return CreatedAtAction(nameof(GetSurveyDetails), new { id = survey.Id }, new {Id = survey.Id });
        }

        private bool ContainsSameAssignedUserIds(ICollection<int> assignedUserIds)
        {
            return assignedUserIds.Count() != assignedUserIds.Distinct().Count();
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

        [HttpPut("surveys/{id}/ready")]
        public async Task<IActionResult> ChangeSurveyStateToReady(int id) 
        {
            var survey = await _surveysRepository.GetSurveyWithAssignedUsers(id);
            if (survey is null)
            {
                return NotFound();
            }
            if (survey.StateId != DraftSurveyStateId)
            {
                return UnprocessableEntity("Survey not in a Draft state");
            }
            if (survey.FormTemplateId == default)
            {
                return UnprocessableEntity("Form template is not assigned");
            }
            if (survey.DeepLinks is null || survey.DeepLinks.Count == 0)
            {
                return UnprocessableEntity("Users are not assigned");
            }
            survey.StateId = ReadySurveyStateId;
            await _surveysRepository.SaveChanges();

            return NoContent();
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
