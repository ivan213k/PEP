using System;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using System.Linq;
using PerformanceEvaluationPlatform.DAL.Repositories.FormsData;
using PerformanceEvaluationPlatform.DAL.Models.FormData.Dto;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormsDataController : ControllerBase
    {
        private readonly IFormDataRepository _formDataRepository;

        public FormsDataController(IFormDataRepository formDataRepository)
        {
            _formDataRepository = formDataRepository ?? throw new ArgumentNullException(nameof(formDataRepository));
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
    }
}
