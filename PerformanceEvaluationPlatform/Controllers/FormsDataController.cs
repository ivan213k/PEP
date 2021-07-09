using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.FormData.ViewModels;
using PerformanceEvaluationPlatform.Models.FormData.RequestModels;
using System.Linq;
using PerformanceEvaluationPlatform.Models.FormData.Enums;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class FormsDataController : ControllerBase
    {

        [Route("forms")]
        public IActionResult Get([FromQuery] FormDataListFilterRequestModel filter)
        {
            var items = GetExampleListItemViewModels();
            items = GetFilteredItems(items, filter);
            return Ok(items);
        }

        private static IEnumerable<FormDataListItemViewModel> GetExampleListItemViewModels()
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
                    State = StateEnum.Active,
                    AppointmentDate = DateTime.Today.AddDays(-4),
                },
                new FormDataListItemViewModel
                {
                    FormName = "Form 2",
                    Assignee = "User 2",
                    AssigneeId = 2,
                    Reviewer = "Admin 2",
                    ReviewerId = 2,
                    State = StateEnum.Blocked,
                    AppointmentDate = DateTime.Today.AddDays(-8),
                },
                new FormDataListItemViewModel
                {
                    FormName = "Form 3",
                    Assignee = "User 3",
                    AssigneeId = 3,
                    Reviewer = "Admin 3",
                    ReviewerId = 3,
                    State = StateEnum.Active,
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

            items = items
                .Skip(filter.Skip.Value)
                .Take(filter.Take.Value);

            return items;
        }

        [Route("forms/states")]
        public IActionResult GetStates()
        {
            var items = new List<FormDataStateListItemViewModel>
            {
                new FormDataStateListItemViewModel
                {
                    State = StateEnum.Active,
                    Name = "Active"
                },
                new FormDataStateListItemViewModel
                {
                    State = StateEnum.Blocked,
                    Name = "Blocked"
                }
            };

            return Ok(items);
        }
    }
}
