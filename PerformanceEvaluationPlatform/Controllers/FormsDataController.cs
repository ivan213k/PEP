using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using System.Linq;
using PerformanceEvaluationPlatform.Models.FormData.Enums;
using PerformanceEvaluationPlatform.DAL.Repositories.FormData;
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
        public IActionResult GetDetailsPage([FromRoute] int id)
        {
            var items = new FormDataDetailsViewModel
            {
                Detail = GetFormDataDetailViewModels(),
                Answers = GetFormDataAnswersItemViewModels(),
            };
            return Ok(items);
        }

        private static FormDataDetailViewModel GetFormDataDetailViewModels()
        {
            var items = new FormDataDetailViewModel
            {
                FormName = "ManualQA",
                FormId = 1,
                Assignee = "Test User 1",
                AssigneeId = 1,
                Reviewer = "Admin User 1",
                ReviewerId = 1,
                State = StateEnum.Draft,
                AppointmentDate = DateTime.Today.AddDays(-4),
                RecommendedLevel = "Middle",
                RecommendedLevelId = 1,
                Project = "Hello Flex",
                ProjectId = 1,
                Team = "Platform",
                TeamId = 1,
                Period = "01.02.2021-01.08.2021",
                ExperienceInCompany = "1 year",
                EnglishLevel = EnglishLevelEnum.B1,
                CurrentPosition = "Junior",
            };
            return items;
        }

        private static ICollection<FormDataAnswersItemViewModel> GetFormDataAnswersItemViewModels()
        {
            var items = new List<FormDataAnswersItemViewModel>
            {
                new FormDataAnswersItemViewModel
                {
                    Skills = "Communication skills",
                    Assessment = "B",
                    Comment = "Any comment",
                    TypeId = 1,
                    TypeName = "Header",
                    Order = 1,
                },
                new FormDataAnswersItemViewModel
                {
                    Skills = "Written communication",
                    Assessment = "C",
                    Comment = "Test comment",
                    TypeId = 2,
                    TypeName = "Row",
                    Order = 2,
                },
                new FormDataAnswersItemViewModel
                {
                    Skills = "Soft skills",
                    Assessment = "B",
                    Comment = "My comment",
                    TypeId = 2,
                    TypeName = "Row",
                    Order = 3,
                }
            };
            return items;
        }
    }
}
