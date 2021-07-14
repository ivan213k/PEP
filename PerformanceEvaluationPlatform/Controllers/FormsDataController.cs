using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using System.Linq;
using PerformanceEvaluationPlatform.Models.FormData.Enums;
using PerformanceEvaluationPlatform.Models.Shared.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormsDataController : ControllerBase
    {

        [HttpGet("forms")]
        public IActionResult Get([FromQuery] FormDataListFilterRequestModel filter)
        {
            var items = GetFormDataListItemViewModels();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        [HttpGet("forms/{id}")]
        public IActionResult GetDetailsPage([FromRoute] int id)
        {
            var items = new FormDataDetailsViewModel
            {
                Detail = GetFormDataDetailViewModels(),
                Answers = GetFormDataAnswersItemViewModels(),
            };
            return Ok(items);
        }

        private static IEnumerable<FormDataListItemViewModel> GetFormDataListItemViewModels()
        {
            var items = new List<FormDataListItemViewModel>
            {
                new FormDataListItemViewModel
                {
                    FormName = "Form 1",
                    Assignee = "User 1",
                    AssigneeId = 1,
                    Reviewer = "Admin 1",
                    ReviewerId = 1,
                    State = StateEnum.Draft,
                    AppointmentDate = DateTime.Today.AddDays(-4),
                },
                new FormDataListItemViewModel
                {
                    FormName = "Form 2",
                    Assignee = "User 2",
                    AssigneeId = 2,
                    Reviewer = "Admin 2",
                    ReviewerId = 2,
                    State = StateEnum.Submitted,
                    AppointmentDate = DateTime.Today.AddDays(-8),
                },
                new FormDataListItemViewModel
                {
                    FormName = "Form 1",
                    Assignee = "User 1",
                    AssigneeId = 3,
                    Reviewer = "Admin 3",
                    ReviewerId = 3,
                    State = StateEnum.Draft,
                    AppointmentDate = DateTime.Now,
                }
            };
            return items;
        }

        private void InitFilter(FormDataListFilterRequestModel filter)
        {
            if (filter.Skip == null)
            {
                filter.Skip = 0;
            }

            if (filter.Take == null)
            {
                filter.Take = 30;
            }
        }

        private IEnumerable<FormDataListItemViewModel> GetFilteredItems(IEnumerable<FormDataListItemViewModel> items,
            FormDataListFilterRequestModel filter)
        {
            InitFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                items = items
                    .Where(fd => fd.FormName.Contains(filter.Search)
                    || fd.Assignee.Contains(filter.Search)
                    || fd.Reviewer.Contains(filter.Search));
            }

            if (filter.State != null && filter.State.Any())
            {
                items = items
                    .Where(fd => filter.State.Contains(fd.State));
            }

            if (filter.AssigneeIds != null)
            {
                items = items
                    .Where(t => filter.AssigneeIds.Contains(t.AssigneeId));
            }

            if (filter.ReviewersIds != null)
            {
                items = items
                    .Where(t => filter.ReviewersIds.Contains(t.ReviewerId));
            }

            if (filter.AppointmentDateFrom != null && filter.AppointmentDateFrom != DateTime.MinValue)
            {
                items = items
                    .Where(fd => fd.AppointmentDate >= filter.AppointmentDateFrom);
            }

            if (filter.AppointmentDateTo != null && filter.AppointmentDateTo != DateTime.MinValue)
            {
                items = items
                    .Where(fd => fd.AppointmentDate <= filter.AppointmentDateTo);
            }

            items = GetOrderedItems(items, filter);

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        [HttpGet("forms/states")]
        public IActionResult GetStates()
        {
            var items = new List<FormDataStateListItemViewModel>
            {
                new FormDataStateListItemViewModel
                {
                    State = StateEnum.Draft,
                    Name = "Draft"
                },

                new FormDataStateListItemViewModel
                {
                    State = StateEnum.Submitted,
                    Name = "Submitted"
                }
            };

            return Ok(items);
        }

        private IEnumerable<FormDataListItemViewModel> GetOrderedItems(IEnumerable<FormDataListItemViewModel> items, FormDataListFilterRequestModel filter)
        {
            if (filter.FormNameOrderBy != null)
            {
                if (filter.FormNameOrderBy == SortOrder.Ascending)
                    items = items.OrderBy(fd => fd.FormName);
                else
                    items = items.OrderByDescending(fd => fd.FormName);
            }
            if (filter.AssigneeNameOrderBy != null)
            {
                if (filter.AssigneeNameOrderBy == SortOrder.Ascending)
                    items = items.OrderBy(fd => fd.Assignee);
                else
                    items = items.OrderByDescending(fd => fd.Assignee);
            }
            return items;
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
