using PerformanceEvaluationPlatform.Application.Interfaces.Surveys;
using PerformanceEvaluationPlatform.Application.Model.Surveys;
using PerformanceEvaluationPlatform.Application.Model.Surveys.Enums;
using PerformanceEvaluationPlatform.Application.Packages;
using PerformanceEvaluationPlatform.Domain.Deeplinks;
using PerformanceEvaluationPlatform.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Surveys
{
    public class SurveyService : ISurveyService
    {
        private const int DraftSurveyStateId = 1;
        private const int ReadySurveyStateId = 2;
        private const int SentSurveyStateId = 3;
        private const int ReadyForReviewSurveyStateId = 4;
        private const int ArchivedSurveyStateId = 5;

        private const int FormDataSubmittedStateId = 2;
        private const int DeepLinkStateDraftId = 1;

        private readonly ISurveysRepository _surveysRepository;

        public SurveyService(ISurveysRepository surveysRepository)
        {
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
        }

        public async Task<ServiceResponse> ChangeStateToArchived(int id)
        {
            var survey = await _surveysRepository.Get(id);
            if (survey is null)
            {
                return ServiceResponse.NotFound();
            }
            if (survey.StateId != ReadyForReviewSurveyStateId)
            {
                return ServiceResponse.UnprocessableEntity("Survey not in a Ready for review state");
            }
            if (!HasSummary(survey))
            {
                return ServiceResponse.UnprocessableEntity("Survey summary is not filled in");
            }
            survey.StateId = ArchivedSurveyStateId;
            await _surveysRepository.SaveChanges();

            return ServiceResponse.NoContent();
        }

        public async Task<ServiceResponse> ChangeStateToReady(int id)
        {
            var survey = await _surveysRepository.GetSurveyWithDeeplinksAndFormData(id);
            if (survey is null)
            {
                return ServiceResponse.NotFound();
            }
            if (survey.StateId != DraftSurveyStateId)
            {
                return ServiceResponse.UnprocessableEntity("Survey not in a Draft state");
            }
            if (survey.FormTemplateId == default)
            {
                return ServiceResponse.UnprocessableEntity("Form template is not assigned");
            }
            if (survey.DeepLinks is null || survey.DeepLinks.Count == 0)
            {
                return ServiceResponse.UnprocessableEntity("Users are not assigned");
            }
            survey.StateId = ReadySurveyStateId;
            await _surveysRepository.SaveChanges();

            return ServiceResponse.NoContent();
        }

        public async Task<ServiceResponse> ChangeStateToReadyForReview(int id)
        {
            var survey = await _surveysRepository.GetSurveyWithDeeplinksAndFormData(id);
            if (survey is null)
            {
                return ServiceResponse.NotFound();
            }
            if (survey.StateId != SentSurveyStateId)
            {
                return ServiceResponse.UnprocessableEntity("Survey not in a Sent state");
            }
            if (HasUnsubmittedFormData(survey))
            {
                return ServiceResponse.UnprocessableEntity("Not all assigned users submitted form data");
            }
            survey.StateId = ReadyForReviewSurveyStateId;
            await _surveysRepository.SaveChanges();

            return ServiceResponse.NoContent();
        }

        public async Task<ServiceResponse> ChangeStateToSent(int id)
        {
            var survey = await _surveysRepository.GetSurveyWithDeeplinksAndFormData(id);
            if (survey is null)
            {
                return ServiceResponse.NotFound();
            }
            if (survey.StateId != DraftSurveyStateId && survey.StateId != ReadySurveyStateId)
            {
                return ServiceResponse.UnprocessableEntity("Survey not in a Draft or Ready state");
            }
            if (survey.StateId == DraftSurveyStateId)
            {
                if (survey.FormTemplateId == default)
                {
                    return ServiceResponse.UnprocessableEntity("Form template is not assigned");
                }
                if (survey.DeepLinks is null || survey.DeepLinks.Count == 0)
                {
                    return ServiceResponse.UnprocessableEntity("Users are not assigned");
                }
            }

            survey.StateId = SentSurveyStateId;
            await _surveysRepository.SaveChanges();

            return ServiceResponse.NoContent();
        }

        public async Task<ServiceResponse<int>> Create(CreateSurveyDto createSurveyDto)
        {
            if (createSurveyDto is null)
            {
                return ServiceResponse<int>.BadRequest();
            }
            //var formTemplate = await _formTemplatesRepository.Get(createSurveyDto.FormId);
            //if (formTemplate is null)
            //{
            //    return ServiceResponse<int>.BadRequest("Form template does not exist.");
            //}
            //var assignee = await _userRepository.Get(createSurveyDto.AssigneeId);
            //if (assignee is null)
            //{
            //    return ServiceResponse<int>.BadRequest("Assignee does not exist.");
            //}
            //var supervisor = await _userRepository.Get(createSurveyDto.SupervisorId);
            //if (supervisor is null)
            //{
            //    return ServiceResponse<int>.BadRequest("Supervisor does not exist.");
            //}
            var level = await _surveysRepository.GetLevel(createSurveyDto.RecommendedLevelId);
            if (level is null)
            {
                return ServiceResponse<int>.BadRequest("Level does not exist.");
            }
            if (ContainsSameAssignedUserIds(createSurveyDto.AssignedUserIds))
            {
                return ServiceResponse<int>.BadRequest($"{nameof(createSurveyDto.AssignedUserIds)} contains same user id");
            }
            //var assignedUsers = await _userRepository.GetList(createSurveyDto.AssignedUserIds);
            //foreach (var assignedUserId in createSurveyDto.AssignedUserIds)
            //{
            //    var assignedUser = assignedUsers.Find(r => r.Id == assignedUserId);
            //    if (assignedUser is null)
            //    {
            //        return ServiceResponse<int>.BadRequest($"Assigned user with id = {assignedUserId}, does not exist.");
            //    }
            //}
            var survey = new Survey
            {
                FormTemplateId = createSurveyDto.FormId,
                AssigneeId = createSurveyDto.AssigneeId,
                SupervisorId = createSurveyDto.SupervisorId,
                RecommendedLevelId = createSurveyDto.RecommendedLevelId,
                AppointmentDate = createSurveyDto.AppointmentDate,
                StateId = DraftSurveyStateId,
                DeepLinks = createSurveyDto.AssignedUserIds
                .Select(userId => new Deeplink
                {
                    Code = Guid.NewGuid(),
                    UserId = userId,
                    StateId = DeepLinkStateDraftId,

                }).ToList()
            };

            await _surveysRepository.Create(survey);
            await _surveysRepository.SaveChanges();

            return ServiceResponse<int>.Success(survey.Id, 201);
        }

        public async Task<ServiceResponse> Update(int id, UpdateSurveyDto updateSurveyDto)
        {
            if (updateSurveyDto is null)
            {
                return ServiceResponse.BadRequest();
            }
            var survey = await _surveysRepository.Get(id);
            if (survey is null)
            {
                return ServiceResponse.NotFound();
            }

            var recommendedLevel = await _surveysRepository.GetLevel(updateSurveyDto.RecommendedLevelId);
            if (recommendedLevel is null)
            {
                return ServiceResponse.BadRequest("Level does not exist.");
            }

            survey.AppointmentDate = updateSurveyDto.AppointmentDate;
            survey.RecommendedLevelId = updateSurveyDto.RecommendedLevelId;
            survey.Summary = updateSurveyDto.Summary;

            await _surveysRepository.SaveChanges();

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse<SurveyDetailsDto>> GetDetails(int id)
        {
            var sureveyDetailsDto = await _surveysRepository.GetDetails(id);
            if (sureveyDetailsDto is null)
            {
                return ServiceResponse<SurveyDetailsDto>.NotFound();
            }

            FillAssignedUsersStatus(sureveyDetailsDto);

            return ServiceResponse<SurveyDetailsDto>.Success(sureveyDetailsDto);
        }

        public async Task<ServiceResponse<IList<SurveyListItemDto>>> GetListItems(SurveyListFilterDto filter)
        {
            var surveys = await _surveysRepository.GetList(filter);
            FillSurveysProgress(surveys);
            return ServiceResponse<IList<SurveyListItemDto>>.Success(surveys);
        }

        public async Task<ServiceResponse<IList<SurveyStateListItemDto>>> GetStateListItems()
        {
            var items = await _surveysRepository.GetStatesList();
            return ServiceResponse<IList<SurveyStateListItemDto>>.Success(items);
        }

        private bool HasSummary(Survey survey)
        {
            return !string.IsNullOrWhiteSpace(survey.Summary);
        }

        private static bool HasUnsubmittedFormData(Survey survey)
        {
            return survey.FormData is null ||
                survey.FormData.Count != survey.DeepLinks.Count ||
                survey.FormData.Any(fd => fd.FormDataStateId != FormDataSubmittedStateId);
        }

        private bool ContainsSameAssignedUserIds(ICollection<int> assignedUserIds)
        {
            return assignedUserIds.Count() != assignedUserIds.Distinct().Count();
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

        private void FillSurveysProgress(IList<SurveyListItemDto> surveys)
        {
            foreach (var survey in surveys)
            {
                survey.ProgressInPercenteges = GetSurveyProgressInPercenteges(survey);
            }
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

        private void FillAssignedUsersStatus(SurveyDetailsDto detailsDto)
        {
            foreach (var assignedUser in detailsDto.AssignedUsers)
            {
                assignedUser.Status = GetFormDataFillStatus(detailsDto.FormData, assignedUser.Id);
            }
        }
    }
}
