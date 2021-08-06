using System;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using System.Linq;
using PerformanceEvaluationPlatform.DAL.Repositories.FormsData;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dto;
using System.Threading.Tasks;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using System.Collections.Generic;
using PerformanceEvaluationPlatform.DAL.Models.Fields.Dao;
using PerformanceEvaluationPlatform.Models.FormData.RequestModel;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dao;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormsDataController : ControllerBase
    {
        private const int InProgressStateId = 2;
        private const int SubmittedStateId = 3;
        private readonly IFormDataRepository _formDataRepository;
        private readonly IFieldsRepository _fieldsRepository;

        public FormsDataController(IFormDataRepository formDataRepository, IFieldsRepository fieldsRepository)
        {
            _formDataRepository = formDataRepository ?? throw new ArgumentNullException(nameof(formDataRepository));
            _fieldsRepository = fieldsRepository ?? throw new ArgumentNullException(nameof(fieldsRepository));
        }

        [HttpGet("forms")]
        public async Task<IActionResult> Get([FromQuery] FormDataListFilterRequestModel filter)
        {
            var filterDto = new FormDataListFilterDto
            {
                Search = filter.Search,
                StateId = filter.StateId,
                AssigneeSortOrder = (int?)filter.AssigneeSortOrder,
                FormNameSortOrder = (int?)filter.FormNameSortOrder,
                AssigneeIds = filter.AssigneeIds,
                ReviewersIds = filter.ReviewersIds,
                AppointmentDateFrom = filter.AppointmentDateFrom,
                AppointmentDateTo = filter.AppointmentDateTo,
                Skip = filter.Skip,
                Take = filter.Take
            };

            var formDataDto = await _formDataRepository.GetList(filterDto);
            var formData = formDataDto.Select(fd => new FormDataListItemViewModel
            {
                Id = fd.Id,
                AssigneeId = fd.AssigneeId,
                Assignee = $"{fd.AssigneeFirstName} {fd.AssigneeLastName}",
                AppointmentDate = fd.AppointmentDate,
                FormId = fd.FormId,
                FormName = fd.FormName,
                Reviewer = $"{fd.ReviewerFirstName} {fd.ReviewerLastName}",
                ReviewerId = fd.ReviewerId,
                State = fd.StateName,
                StateId = fd.StateId
            });
            return Ok(formData);
        }

        [HttpGet("forms/states")]
        public async Task<IActionResult> GetStates()
        {
            var itemsDto = await _formDataRepository.GetStatesList();
            var items = itemsDto
                .Select(t => new FormDataStateListItemViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return Ok(items);
        }

        [HttpGet("forms/{id:int}")]
        public async Task<IActionResult> GetDetailsPage([FromRoute] int id)
        {
            var detailsDto = await _formDataRepository.GetDetails(id);
            if (detailsDto == null)
            {
                return NotFound();
            }
            var detailsViewModel = new FormDataDetailViewModel
            {
                FormId = detailsDto.FormId,
                FormName = detailsDto.FormName,
                Assignee = detailsDto.Assignee,
                AssigneeId = detailsDto.AssigneeId,
                Reviewer = detailsDto.Reviewer,
                ReviewerId = detailsDto.ReviewerId,
                AppointmentDate = detailsDto.AppointmentDate,
                RecommendedLevel = detailsDto.RecommendedLevel,
                RecommendedLevelId = detailsDto.RecommendedLevelId,
                State = detailsDto.State,
                StateId = detailsDto.FormDataStateId,
                Project = detailsDto.Project,
                ProjectId = detailsDto.ProjectId,
                Team = detailsDto.Team,
                TeamId = detailsDto.TeamId,
                Period = detailsDto.Period,
                ExperienceInCompany = detailsDto.ExperienceInCompany,
                EnglishLevel = detailsDto.EnglishLevel,
                CurrentPosition = detailsDto.CurrentPosition,
                Answers = detailsDto.Answers?
                .Select(fd => new FormDataAnswersItemViewModel
                {
                    Assessment = fd.Assessment,
                    Comment = fd.Comment,
                    TypeId = fd.TypeId,
                    TypeName = fd.TypeName,
                    Order = fd.Order,
                }).ToList()
            };
            return Ok(detailsViewModel);
        }

        [HttpPut("forms/{formDataId:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] IList<UpdateFieldDataRequestModel> requestModel)
        {
            var entity = await _formDataRepository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            if (entity.FormDataStateId == SubmittedStateId)
            {
                return UnprocessableEntity("This form already complited");
            }

            foreach (var item in requestModel)
            {
                var field = await _fieldsRepository.Get(item.FieldId);
                if (field == null)
                {
                    return BadRequest("Field does not exists.");
                }
            }

            foreach (var item in requestModel)
            {
                var assessment = await _fieldsRepository.Get(item.AssesmentId);
                if (assessment == null)
                {
                    return BadRequest("Assesment does not exists.");
                }
                if (assessment.AssesmentGroupId != entity.FieldData.Select(x=>x.Field.AssesmentGroupId).FirstOrDefault())
                {
                    return BadRequest("Assessment is not related to the field");
                }
            }

            foreach (var item in entity.FieldData)
            {
                if (item.Assesment.IsCommentRequired && string.IsNullOrWhiteSpace(item.Comment))
                {
                    return BadRequest("Comment is required, but not set");
                }
            }

            entity.FieldData.Clear();
            entity.FieldData = requestModel.Select(m => new FieldData
            {
                Comment = m.Comment,
                FieldId = m.FieldId,
                AssesmentId = m.AssesmentId
            }).ToList();

            entity.FormDataStateId = InProgressStateId;

            await _formDataRepository.SaveChanges();
            return Ok("State was changed on In Progress");
        }
    }
}